import 'package:flutter/material.dart';
import '../widgets/video_item.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: ListView.builder(
        itemCount: 4,
        itemBuilder: (context, index) {
          return VideoItem(
            title: 'Кто Найдёт Больше Всего Дорогих Вещей Челлендж ! (Бустер, Куертов, Кореш, Парадеич, Кокошка, Лимба) $index',
            channelName: 'ExileShow',
            views: '${1000 * (index + 1)} просмотров',
            timeAgo: '${index + 1} дня назад',
            thumbnailUrl: 'https://i.pinimg.com/736x/ac/9e/2a/ac9e2ade1537c8a48c679ce51a7846da.jpg',
            avatarUrl: 'https://yt3.googleusercontent.com/MW4JvUKyyL_3Gh9gsnlMdNGBjjmFlV4wITdlpM3bt4uGPpTdyAK3bAyJayWMZoUPqz45qFpqp2o=s160-c-k-c0x00ffffff-no-rj',
          );
        },
      ),
    );
  }
}
