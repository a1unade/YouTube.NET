import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/auth_response.dart';
import '../models/login_response.dart';
import '../models/wallet_response.dart';

class AuthService {
  Future<AuthResponse> register({
    required String email,
    required String password,
    required String name,
    required String surname,
    required String gender,
    required String dateOfBirth, // ожидаем в формате 'дд.мм.гггг'
    required String country,
  }) async {
    final parts = dateOfBirth.split('.');
    if (parts.length != 3) {
      throw Exception('Неверный формат даты рождения');
    }
    final birthDate = DateTime(
      int.parse(parts[2]),
      int.parse(parts[1]),
      int.parse(parts[0]),
    );
    final birthDateIso = birthDate.toUtc().toIso8601String();

    final uri = Uri.parse('http://localhost:8080/Auth/Auth');
    final response = await http.post(
      uri,
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({
        'email': email,
        'password': password,
        'name': name,
        'surname': surname,
        'gender': gender,
        'dateOfBirth': birthDateIso,
        'country': country,
      }),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      return AuthResponse.fromJson(data);
    } else {
      throw Exception('Ошибка регистрации: ${response.body}');
    }
  }

  Future<LoginResponse> login({
    required String email,
    required String password,
  }) async {
    final url = Uri.parse('http://localhost:8080/Auth/Login');

    final response = await http.post(
      url,
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'email': email, 'password': password}),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      return LoginResponse.fromJson(data);
    } else {
      throw Exception('Ошибка логина: ${response.body}');
    }
  }

  Future<WalletResponse> createWallet({required String id, required double balance}) async {
    final uri = Uri.parse('http://localhost:8070/graphql');
    final query = '''
      mutation {
        createWallet(id: "$id", balance: $balance) {
          isSuccessfully
          message
          entityId
        }
      }
    ''';

    final response = await http.post(
      uri,
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'query': query}),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      final walletData = data['data']['createWallet'];
      return WalletResponse.fromJson(walletData);
    } else {
      throw Exception('Ошибка создания кошелька: ${response.body}');
    }
  }
}
