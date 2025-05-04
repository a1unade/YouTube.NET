import 'package:flutter/material.dart';

class AuthProvider extends ChangeNotifier {
  bool _isLoggedIn = true;

  bool get isLoggedIn => _isLoggedIn;

  void logOut() {
    _isLoggedIn = false;
    notifyListeners();
  }

  void logIn() {
    _isLoggedIn = true;
    notifyListeners();
  }
}
