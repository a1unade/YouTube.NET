import 'package:flutter/material.dart';
import '../modals/video_model.dart';

class VideoItem extends StatelessWidget {
  final String title;
  final String channelName;
  final String views;
  final String timeAgo;
  final String thumbnailUrl;
  final String avatarUrl;
  final String videoUrl;

  const VideoItem({
    required this.title,
    required this.channelName,
    required this.views,
    required this.timeAgo,
    required this.thumbnailUrl,
    required this.avatarUrl,
    required this.videoUrl,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        final video = VideoModel(
          title: title,
          channelName: channelName,
          views: views,
          timeAgo: timeAgo,
          thumbnailUrl: thumbnailUrl,
          avatarUrl: avatarUrl,
          videoUrl: videoUrl,
        );

        Navigator.pushNamed(
          context,
          '/video',
          arguments: video,
        );
      },
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          AspectRatio(
            aspectRatio: 16 / 9,
            child: Image.network(
              thumbnailUrl,
              fit: BoxFit.cover,
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                CircleAvatar(
                  radius: 20,
                  backgroundImage: NetworkImage(avatarUrl),
                ),
                SizedBox(width: 8),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        title,
                        maxLines: 2,
                        overflow: TextOverflow.ellipsis,
                        style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                      ),
                      SizedBox(height: 4),
                      Text(
                        '$channelName · $views · $timeAgo',
                        style: TextStyle(color: Colors.grey[600], fontSize: 13),
                      ),
                    ],
                  ),
                ),
                Icon(Icons.more_vert, color: Colors.white),
              ],
            ),
          ),
          SizedBox(height: 8),
        ],
      ),
    );
  }
}
