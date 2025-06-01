import 'package:flutter_bloc/flutter_bloc.dart';
import '../../models/chat_model.dart';

// Состояния
abstract class ChatListState {}

class ChatListInitial extends ChatListState {}

class ChatListLoading extends ChatListState {}

class ChatListLoaded extends ChatListState {
  final List<ChatSummary> chats;

  ChatListLoaded(this.chats);
}

class ChatListError extends ChatListState {
  final String message;

  ChatListError(this.message);
}
