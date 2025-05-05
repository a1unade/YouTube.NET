import 'package:flutter/material.dart';
import '../modals/video_model.dart';
import '../widgets/video_player_widget.dart';
import '../widgets/channel_info_row.dart';
import '../widgets/comment_section.dart';
import '../widgets/related_videos_list.dart';
import '../widgets/video_action_bar.dart';

class VideoDetailPage extends StatelessWidget {
  final VideoModel video;

  const VideoDetailPage({super.key, required this.video});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: SafeArea(
        child: ListView(
          children: [
            VideoPlayerWidget(videoUrl: video.videoUrl),
            Padding(
              padding: const EdgeInsets.all(12.0),
              child: Text(
                video.title,
                style: TextStyle(color: Colors.black, fontSize: 18, fontWeight: FontWeight.bold),
              ),
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 12.0),
              child: Text(
                '${video.views} · ${video.timeAgo}',
                style: TextStyle(color: Colors.grey),
              ),
            ),
            ChannelInfoRow(video: video),
            VideoActionBar(),
            Divider(color: Colors.grey.shade800),
            CommentSection(),
            RelatedVideosList(),
          ],
        ),
      ),
    );
  }
}
