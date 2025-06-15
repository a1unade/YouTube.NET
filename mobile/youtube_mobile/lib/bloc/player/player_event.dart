abstract class PlayerEvent {}

class LoadVideo extends PlayerEvent {
  final String videoId;

  LoadVideo(this.videoId);
}

class ViewCountUpdated extends PlayerEvent {
  final int viewCount;
  ViewCountUpdated(this.viewCount);
}