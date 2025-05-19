import 'package:flutter_bloc/flutter_bloc.dart';
import 'home_event.dart';
import 'home_state.dart';
import '../../services/video_service.dart';

class HomeBloc extends Bloc<HomeEvent, HomeState> {
  final VideoService service;

  HomeBloc(this.service) : super(HomeInitial()) {
    on<LoadVideos>((event, emit) async {
      emit(HomeLoading());
      try {
        final videos = await service.fetchVideos(page: event.page);
        emit(HomeLoaded(videos));
      } catch (e) {
        emit(HomeError(e.toString()));
      }
    });
  }
}
