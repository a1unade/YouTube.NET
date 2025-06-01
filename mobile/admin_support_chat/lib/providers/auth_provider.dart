import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthProvider extends ChangeNotifier {
  String? _userId;
  bool _isLoading = true;

  String? get userId => _userId;
  bool get isLoggedIn => _userId != null;
  bool get isLoading => _isLoading;

  Future<void> registerWithUserId(String userId) async {
    _userId = userId;
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString('user_id', userId);
    notifyListeners();
  }

  Future<void> logInWithUserId(String userId) async {
    _userId = userId;
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString('user_id', userId);

    notifyListeners();
  }

  Future<void> tryAutoLogin() async {
    _isLoading = true;
    notifyListeners();

    final prefs = await SharedPreferences.getInstance();
    _userId = prefs.getString('user_id');

    _isLoading = false;
    notifyListeners();
  }

  Future<void> logOut() async {
    _userId = null;
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('user_id');
    await prefs.remove('wallet_id');
    notifyListeners();
  }
}

