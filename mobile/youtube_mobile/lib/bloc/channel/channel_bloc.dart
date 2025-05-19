import 'package:flutter_bloc/flutter_bloc.dart';
import '../../services/channel_service.dart';
import 'channel_event.dart';
import 'channel_state.dart';

class ChannelBloc extends Bloc<ChannelEvent, ChannelState> {
  final ChannelService service;
  ChannelBloc(this.service) : super(ChannelInitial()) {
    on<LoadChannel>((event, emit) async {
      emit(ChannelLoading());
      try {
        final data = await service.fetchChannelById(event.channelId);
        emit(ChannelLoaded(data));
      } catch (e) {
        emit(ChannelError(e.toString()));
      }
    });
  }
}