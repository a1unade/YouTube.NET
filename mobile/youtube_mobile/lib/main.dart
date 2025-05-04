import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:youtube_mobile/providers/auth_provider.dart';
import 'package:youtube_mobile/screens/main_navigation_screen.dart';

void main() {
  runApp(
    ChangeNotifierProvider(
      create: (_) => AuthProvider(),
      child: MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'YouTube Premium',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(primarySwatch: Colors.red),
      // home: const PremiumPage(userId: 'user123', balanceId: 'balance456'),
      home: MainNavigationScreen(),
    );
  }
}
