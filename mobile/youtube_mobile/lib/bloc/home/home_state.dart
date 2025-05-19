import '../../models/video_model.dart';

abstract class HomeState {}

class HomeInitial extends HomeState {}

class HomeLoading extends HomeState {}

class HomeLoaded extends HomeState {
  final List<Video> videos;
  HomeLoaded(this.videos);
}

class HomeError extends HomeState {
  final String message;
  HomeError(this.message);
}
