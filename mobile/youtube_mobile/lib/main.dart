import 'package:flutter/material.dart';
import 'screens/premium_page.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'YouTube Premium',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(primarySwatch: Colors.red),
      home: const PremiumPage(userId: 'user123', balanceId: 'balance456'),
    );
  }
}
