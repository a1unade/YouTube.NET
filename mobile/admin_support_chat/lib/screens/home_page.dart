import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../bloc/chat_list/chat_list_state.dart';
import '../bloc/chat_list/chat_list_bloc.dart';
import '../bloc/chat_list/chat_list_event.dart';
import '../providers/auth_provider.dart';
import 'package:admin_support_chat/screens/chat_screen.dart';
import 'package:provider/provider.dart';


class HomePage extends StatefulWidget {
  final ChatListBloc bloc;

  const HomePage({super.key, required this.bloc});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  @override
  void initState() {
    super.initState();
    widget.bloc.add(LoadChats());
  }

  @override
  Widget build(BuildContext context) {
    final userId = Provider.of<AuthProvider>(context).userId;

    if (userId == null || userId.isEmpty) {
      return Scaffold(
        appBar: AppBar(title: const Text('Список чатов поддержки')),
        body: const Center(
          child: Text('Пожалуйста, авторизуйтесь, чтобы увидеть чаты.'),
        ),
      );
    }

    return Scaffold(
      appBar: AppBar(title: const Text('Список чатов поддержки')),
      body: BlocProvider.value(
        value: widget.bloc,
        child: BlocBuilder<ChatListBloc, ChatListState>(
          builder: (context, state) {
            if (state is ChatListLoading) {
              return const Center(child: CircularProgressIndicator());
            } else if (state is ChatListLoaded) {
              if (state.chats.isEmpty) {
                return const Center(child: Text('Чатов пока нет'));
              }
              return ListView.builder(
                itemCount: state.chats.length,
                itemBuilder: (_, index) {
                  final chat = state.chats[index];
                  return ListTile(
                    title: Text('Чат: ${chat.chatId}'),
                    subtitle: Text(chat.lastMessage ?? 'Без сообщений'),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (_) => ChatScreen(
                            userId: userId,
                            chatId: chat.chatId,
                          ),
                        ),
                      );
                    },
                  );
                },
              );
            } else if (state is ChatListError) {
              return Center(child: Text('Ошибка: ${state.message}'));
            }
            return Container();
          },
        ),
      ),
    );
  }
}
