import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../bloc/player/player_bloc.dart';
import '../bloc/player/player_event.dart';
import '../bloc/player/player_state.dart';
import '../services/player_service.dart';
import '../widgets/video_player_widget.dart';
import '../widgets/channel_info_row.dart';
import '../widgets/comment_section.dart';
import '../widgets/video_action_bar.dart';
import '../services/rabbitmq_service.dart';

class VideoDetailPage extends StatelessWidget {
  final String videoId;
  final Map<String, dynamic> channelData;

  const VideoDetailPage({
    super.key,
    required this.videoId,
    required this.channelData,
  });

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (_) => PlayerBloc(PlayerService(), RabbitMqService()..connect())
        ..add(LoadVideo(videoId)),
      child: Scaffold(
        backgroundColor: Colors.white,
        body: SafeArea(
          child: BlocBuilder<PlayerBloc, PlayerState>(
            builder: (context, state) {
              if (state is PlayerLoading) {
                return const Center(child: CircularProgressIndicator());
              } else if (state is PlayerLoaded) {
                final video = state.video;
                return ListView(
                  children: [
                    VideoPlayerWidget(
                      videoUrl: 'http://localhost:8080/File/GetVideoStream/${video.videoFileId}',
                    ),
                    Padding(
                      padding: const EdgeInsets.all(12.0),
                      child: Text(
                        video.name,
                        style: const TextStyle(
                          color: Colors.black,
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 12.0),
                      child: Text(
                        '${state.viewCount} · ${video.realiseDate}',
                        style: const TextStyle(color: Colors.grey),
                      ),
                    ),
                    ChannelInfoRow(channelData: channelData),
                    VideoActionBar(),
                    Divider(color: Colors.grey.shade800),
                    const CommentSection(),
                  ],
                );
              } else if (state is PlayerError) {
                return Center(child: Text('Ошибка: ${state.message}'));
              }
              return const SizedBox.shrink();
            },
          ),
        ),
      ),
    );
  }
}
