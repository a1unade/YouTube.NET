import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/chat_model.dart';

class ChatListService {
  final String baseUrl;

  ChatListService({required this.baseUrl});

  Future<List<ChatSummary>> getChats(int page, int size) async {
    final response = await http.get(
      Uri.parse('http://localhost:8080/Chat/GetHistoryCollectionForCard?Page=1&Size=10'),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      print('Decoded: $data');

      final chatListRaw = data['chatCardDtos'];
      if (chatListRaw is List) {
        print('chatListRaw: $chatListRaw');
        return chatListRaw.map((e) => ChatSummary.fromJson(e)).toList();
      } else {
        throw Exception('chatCardDtos is not a List');
      }
    } else {
      throw Exception('Ошибка загрузки чатов');
    }
  }
}
