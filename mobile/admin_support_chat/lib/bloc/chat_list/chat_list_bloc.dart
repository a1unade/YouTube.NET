import 'package:flutter_bloc/flutter_bloc.dart';
import '../../services/chat_list_service.dart';
import 'chat_list_event.dart';
import 'chat_list_state.dart';


// BLoC
class ChatListBloc extends Bloc<ChatListEvent, ChatListState> {
  final ChatListService service;

  ChatListBloc(this.service) : super(ChatListInitial()) {
    on<LoadChats>((event, emit) async {
      emit(ChatListLoading());
      try {
        final chats = await service.getChats(event.page, event.size);
        emit(ChatListLoaded(chats));
      } catch (e) {
        emit(ChatListError(e.toString()));
      }
    });
  }
}