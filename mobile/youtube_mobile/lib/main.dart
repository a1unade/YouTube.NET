import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:youtube_mobile/providers/auth_provider.dart';
import 'package:youtube_mobile/screens/main_navigation_screen.dart';
import 'package:youtube_mobile/screens/video_detail_page.dart';

import 'modals/video_model.dart';

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
      title: 'YouTube',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(primarySwatch: Colors.red),
      home: MainNavigationScreen(),
      onGenerateRoute: (settings) {
        if (settings.name == '/video') {
          final video = settings.arguments as VideoModel;
          return MaterialPageRoute(
            builder: (_) => VideoDetailPage(video: video),
          );
        }
        return null;
      },
    );
  }
}
