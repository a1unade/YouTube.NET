import 'package:flutter/material.dart';
import 'package:admin_support_chat/screens/profile_screen.dart';
import 'package:admin_support_chat/widgets/app_bar_youtube.dart';
import '../widgets/bottom_nav_bar.dart';
import 'home_page.dart';
import '../bloc/chat_list/chat_list_bloc.dart';
import '../services/chat_list_service.dart';

class MainNavigationScreen extends StatefulWidget {
  const MainNavigationScreen({super.key});

  @override
  _MainNavigationScreenState createState() => _MainNavigationScreenState();
}

class _MainNavigationScreenState extends State<MainNavigationScreen> {
  int _selectedIndex = 0;
  late final ChatListBloc _chatListBloc;

  @override
  void initState() {
    super.initState();
    _chatListBloc = ChatListBloc(ChatListService(baseUrl: 'http://localhost:8080'));
  }

  List<Widget> get _pages => [
    HomePage(bloc: _chatListBloc),
    ProfileScreen(),
  ];

  @override
  void dispose() {
    _chatListBloc.close();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: YouTubeAppBar(),
      body: _pages[_selectedIndex],
      bottomNavigationBar: YouTubeBottomNavBar(
        currentIndex: _selectedIndex,
        onTap: (index) => setState(() => _selectedIndex = index),
      ),
    );
  }
}

