import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import 'package:youtube_mobile/providers/auth_provider.dart';
import 'package:youtube_mobile/screens/main_navigation_screen.dart';
import 'package:youtube_mobile/screens/video_detail_page.dart';

import 'bloc/user/user_bloc.dart';
import 'services/user_service.dart';

void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => AuthProvider()),
      ],
      child: MultiBlocProvider(
        providers: [
          BlocProvider<UserBloc>(
            create: (_) => UserBloc(UserService()),
          ),
        ],
        child: const MyApp(),
      ),
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
      home: const MainNavigationScreen(),
      onGenerateRoute: (settings) {
        if (settings.name == '/video') {
          final args = settings.arguments as Map<String, dynamic>;
          final String videoId = args['videoId'] as String;
          final Map<String, dynamic> channelData = args['channelData'] as Map<String, dynamic>;

          return MaterialPageRoute(
            builder: (_) => VideoDetailPage(
              videoId: videoId,
              channelData: channelData,
            ),
          );
        }
        return null;
      },
    );
  }
}
