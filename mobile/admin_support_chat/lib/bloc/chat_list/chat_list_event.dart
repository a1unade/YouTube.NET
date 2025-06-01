import 'package:flutter_bloc/flutter_bloc.dart';

// События
abstract class ChatListEvent {}

class LoadChats extends ChatListEvent {
  final int page;
  final int size;

  LoadChats({this.page = 1, this.size = 10});
}