import '../../models/player_video_model.dart';

abstract class PlayerState {}

class PlayerInitial extends PlayerState {}
class PlayerLoading extends PlayerState {}

class PlayerLoaded extends PlayerState {
  final PlayerVideo video;
  final int viewCount;

  PlayerLoaded(this.video, this.viewCount);
}


class PlayerError extends PlayerState {
  final String message;

  PlayerError(this.message);
}