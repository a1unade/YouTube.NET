import 'package:flutter/material.dart';
import 'package:youtube_mobile/screens/profile_screen.dart';
import 'package:youtube_mobile/screens/subscriptions_screen.dart';
import 'package:youtube_mobile/widgets/app_bar_youtube.dart';
import '../widgets/bottom_nav_bar.dart';
import 'premium_page.dart';
import 'home_page.dart';

class MainNavigationScreen extends StatefulWidget {
  const MainNavigationScreen({super.key});

  @override
  _MainNavigationScreenState createState() => _MainNavigationScreenState();
}

class _MainNavigationScreenState extends State<MainNavigationScreen> {
  int _selectedIndex = 0;

  final List<Widget> _pages = [
    HomePage(),
    SubscriptionsScreen(),
    ProfileScreen()
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: YouTubeAppBar(),
      body: _pages[_selectedIndex],
      bottomNavigationBar: YouTubeBottomNavBar(
        currentIndex: _selectedIndex,
        onTap: (index) {
          setState(() {
            _selectedIndex = index;
          });
        },
      ),
    );
  }
}
