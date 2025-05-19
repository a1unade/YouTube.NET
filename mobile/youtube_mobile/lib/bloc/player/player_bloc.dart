import 'package:flutter_bloc/flutter_bloc.dart';
import '../../services/player_service.dart';
import 'player_event.dart';
import 'player_state.dart';

class PlayerBloc extends Bloc<PlayerEvent, PlayerState> {
  final PlayerService service;

  PlayerBloc(this.service) : super(PlayerInitial()) {
    on<LoadVideo>((event, emit) async {
      emit(PlayerLoading());
      try {
        final video = await service.fetchVideoById(event.videoId);
        emit(PlayerLoaded(video));
      } catch (e) {
        emit(PlayerError(e.toString()));
      }
    });
  }
}