abstract class HomeEvent {}

class LoadVideos extends HomeEvent {
  final int page;
  LoadVideos({this.page = 1});
}
