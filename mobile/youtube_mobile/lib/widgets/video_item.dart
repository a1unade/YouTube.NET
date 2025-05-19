import 'package:flutter/material.dart';
import '../models/video_model.dart';

import 'package:flutter_bloc/flutter_bloc.dart';
import '../bloc/channel/channel_bloc.dart';
import '../bloc/channel/channel_event.dart';
import '../bloc/channel/channel_state.dart';
import '../services/channel_service.dart';

class VideoItem extends StatelessWidget {
  final Video video;

  const VideoItem({super.key, required this.video});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (_) => ChannelBloc(ChannelService())..add(LoadChannel(video.channelId)),
      child: Builder(
        builder: (context) => GestureDetector(
          onTap: () {
            final channelState = context.read<ChannelBloc>().state;
            Map<String, dynamic>? channelData;
            if (channelState is ChannelLoaded) {
              channelData = channelState.channelData;
            }

            Navigator.pushNamed(
              context,
              '/video',
              arguments: {
                'videoId': video.videoId,
                'channelData': channelData,
              },
            );
          },
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              AspectRatio(
                aspectRatio: 16 / 9,
                child: Image.network(
                  video.previewUrl,
                  fit: BoxFit.cover,
                ),
              ),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    BlocBuilder<ChannelBloc, ChannelState>(
                      builder: (context, state) {
                        if (state is ChannelLoaded) {
                          return CircleAvatar(
                            radius: 20,
                            backgroundImage: NetworkImage(
                              'http://localhost:8080/File/GetFileStream/${state.channelData['mainImage']}' ?? video.channelImageUrl,
                            ),
                          );
                        } else if (state is ChannelLoading) {
                          return CircleAvatar(
                            radius: 20,
                            backgroundColor: Colors.grey[300],
                            child: CircularProgressIndicator(strokeWidth: 2),
                          );
                        } else {
                          return CircleAvatar(
                            radius: 20,
                            backgroundImage: NetworkImage(video.channelImageUrl),
                          );
                        }
                      },
                    ),
                    SizedBox(width: 8),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            video.videoName,
                            maxLines: 2,
                            overflow: TextOverflow.ellipsis,
                            style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                          ),
                          SizedBox(height: 4),
                          BlocBuilder<ChannelBloc, ChannelState>(
                            builder: (context, state) {
                              String channelName = video.channelId;
                              String subs = '';
                              if (state is ChannelLoaded) {
                                channelName = state.channelData['name'] ?? channelName;
                                subs = _formatSubscribers(state.channelData['subscribers']);
                              }
                              return Text(
                                '$channelName $subs · ${video.views} views · ${video.releaseDate}',
                                style: TextStyle(color: Colors.grey[600], fontSize: 13),
                              );
                            },
                          ),
                        ],
                      ),
                    ),
                    Icon(Icons.more_vert, color: Colors.grey[700]),
                  ],
                ),
              ),
              SizedBox(height: 8),
            ],
          ),
        ),
      ),
    );
  }

  String _formatSubscribers(int? subs) {
    if (subs == null) return '';
    if (subs >= 1000000) return '${(subs / 1000000).toStringAsFixed(1)}M subscribers';
    if (subs >= 1000) return '${(subs / 1000).toStringAsFixed(1)}K subscribers';
    return '$subs subscribers';
  }
}
