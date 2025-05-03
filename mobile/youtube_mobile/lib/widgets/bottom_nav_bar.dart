import 'package:flutter/material.dart';

class YouTubeBottomNavBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BottomNavigationBar(
      type: BottomNavigationBarType.fixed,
      selectedItemColor: Colors.white,
      unselectedItemColor: Colors.grey,
      backgroundColor: Colors.black,
      items: const [
        BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Главная'),
        BottomNavigationBarItem(icon: Icon(Icons.play_arrow), label: 'Shorts'),
        BottomNavigationBarItem(icon: Icon(Icons.add_circle_outline), label: ''),
        BottomNavigationBarItem(icon: Icon(Icons.subscriptions), label: 'Подписки'),
        BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Вы'),
      ],
    );
  }
}
