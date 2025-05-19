import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../bloc/home/home_state.dart';
import '../bloc/home/home_bloc.dart';
import '../bloc/home/home_event.dart';
import '../services/video_service.dart';
import '../widgets/video_item.dart';
import '../models/video_model.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (_) => HomeBloc(VideoService())..add(LoadVideos()),
      child: Scaffold(
        body: BlocBuilder<HomeBloc, HomeState>(
          builder: (context, state) {
            if (state is HomeLoading) {
              return Center(child: CircularProgressIndicator());
            } else if (state is HomeLoaded) {
              return ListView.builder(
                itemCount: state.videos.length,
                itemBuilder: (context, index) {
                  final v = state.videos[index];

                  return VideoItem(
                    video: Video(
                      previewUrl: 'http://localhost:8080/File/GetFileStream/${v.previewUrl}',
                      channelImageUrl: 'http://localhost:8080/File/GetFileStream/${v.channelImageUrl}',
                      videoName: v.videoName,
                      views: v.views,
                      releaseDate: v.releaseDate,
                      videoId: v.videoId,
                      channelId: v.channelId,
                    ),
                  );
                },
              );
            } else if (state is HomeError) {
              return Center(child: Text('Ошибка: ${state.message}'));
            }

            return SizedBox.shrink();
          },
        ),
      ),
    );
  }
}
