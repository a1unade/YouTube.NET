import 'package:flutter_bloc/flutter_bloc.dart';
import '../../services/player_service.dart';
import 'player_event.dart';
import 'player_state.dart';
import '../../services/rabbitmq_service.dart';
import 'dart:async';

class PlayerBloc extends Bloc<PlayerEvent, PlayerState> {
  final PlayerService service;
  final RabbitMqService mq;

  StreamSubscription? _mqSub;

  PlayerBloc(this.service, this.mq) : super(PlayerInitial()) {
    on<LoadVideo>(_onLoadVideo);
    on<ViewCountUpdated>(_onViewCountUpdated);
  }

  Future<void> _onLoadVideo(
      LoadVideo e, Emitter<PlayerState> emit) async {
    emit(PlayerLoading());

    try {
      final video = await service.fetchVideoById(e.videoId);

      // 1) подписка на RabbitMQ
      _mqSub ??= _initMq(e.videoId);

      // 2) POST /Video/IncrementView
      await service.incrementView(e.videoId);

      emit(PlayerLoaded(video, video.viewCount));
    } catch (err) {
      emit(PlayerError(err.toString()));
    }
  }

  void _onViewCountUpdated(
      ViewCountUpdated e, Emitter<PlayerState> emit) {
    if (state is PlayerLoaded) {
      final s = state as PlayerLoaded;
      emit(PlayerLoaded(s.video, e.viewCount));
    }
  }

  StreamSubscription _initMq(String videoId) {
    return mq.stream.listen((msg) {
      print('[RabbitMQ] videoId: ${msg.videoId}, views: ${msg.viewCount}');

      if (msg.videoId.replaceAll('-', '') == videoId) {
        add(ViewCountUpdated(msg.viewCount));
      }
    });
  }

  @override
  Future<void> close() {
    _mqSub?.cancel();
    return super.close();
  }
}
