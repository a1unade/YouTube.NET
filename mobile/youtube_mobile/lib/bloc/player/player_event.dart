abstract class PlayerEvent {}

class LoadVideo extends PlayerEvent {
  final String videoId;

  LoadVideo(this.videoId);
}