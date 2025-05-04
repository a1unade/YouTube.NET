import 'package:flutter/material.dart';
import '../modals/video_model.dart';
import 'video_item.dart';

class RelatedVideosList extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final relatedVideos = List.generate(3, (index) => VideoModel(
        title: 'Кто Найдёт Больше Всего Дорогих Вещей Челлендж ! (Бустер, Куертов, Кореш, Парадеич, Кокошка, Лимба) $index',
        channelName: 'ExileShow',
        views: '${(index + 1) * 1000} просмотров',
        timeAgo: '${index + 2} дня назад',
        thumbnailUrl: 'https://i.pinimg.com/736x/ac/9e/2a/ac9e2ade1537c8a48c679ce51a7846da.jpg',
        avatarUrl: 'https://yt3.googleusercontent.com/MW4JvUKyyL_3Gh9gsnlMdNGBjjmFlV4wITdlpM3bt4uGPpTdyAK3bAyJayWMZoUPqz45qFpqp2o=s160-c-k-c0x00ffffff-no-rj',
        videoUrl: 'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4'
    ));

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: relatedVideos.map((video) {
        return Padding(
          padding: const EdgeInsets.symmetric(horizontal: 12.0),
          child: VideoItem(
            title: video.title,
            channelName: video.channelName,
            views: video.views,
            timeAgo: video.timeAgo,
            thumbnailUrl: video.thumbnailUrl,
            avatarUrl: video.avatarUrl,
            videoUrl: video.videoUrl,
          ),
        );
      }).toList(),
    );
  }
}
