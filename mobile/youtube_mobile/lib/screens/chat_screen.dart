import 'dart:async';
import 'package:flutter/material.dart';
import 'package:uuid/uuid.dart';

import '../services/chat_service.dart';
import '../src/generated/chat.pb.dart';

class ChatScreen extends StatefulWidget {
  const ChatScreen({super.key});

  @override
  State<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends State<ChatScreen> {
  final TextEditingController _controller = TextEditingController();
  final List<ChatMessageResponse> _messages = [];
  late final ChatGrpcClient _client;
  late final String _userId;
  String? _chatId;
  StreamSubscription<ChatMessageResponse>? _streamSubscription;

  @override
  void initState() {
    super.initState();
    _userId = const Uuid().v4(); // или получай из логина
    _client = ChatGrpcClient('10.0.2.2', 8081); // Android: 10.0.2.2 = localhost
    _initChat();
  }

  Future<void> _initChat() async {
    final response = await _client.joinChat(_userId);
    _chatId = response.chatId;
    _listenToMessages();
  }

  void _listenToMessages() {
    _streamSubscription = _client.messageStream(_chatId!, _userId).listen((message) {
      setState(() {
        _messages.insert(0, message);
      });
    });
  }

  Future<void> _sendMessage() async {
    final text = _controller.text.trim();
    if (text.isEmpty || _chatId == null) return;
    await _client.sendMessage(_chatId!, _userId, text);
    _controller.clear();
  }

  @override
  void dispose() {
    _streamSubscription?.cancel();
    _client.shutdown();
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Чат с поддержкой")),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              reverse: true,
              padding: const EdgeInsets.all(8),
              itemCount: _messages.length,
              itemBuilder: (_, i) {
                final msg = _messages[i];
                final isMine = msg.userId == _userId;
                return Align(
                  alignment: isMine ? Alignment.centerRight : Alignment.centerLeft,
                  child: Container(
                    padding: const EdgeInsets.all(12),
                    margin: const EdgeInsets.symmetric(vertical: 4),
                    decoration: BoxDecoration(
                      color: isMine ? Colors.blue : Colors.grey[300],
                      borderRadius: BorderRadius.circular(12),
                    ),
                    child: Text(
                      msg.message,
                      style: TextStyle(
                        color: isMine ? Colors.white : Colors.black,
                      ),
                    ),
                  ),
                );
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: _controller,
                    decoration: const InputDecoration(hintText: "Введите сообщение"),
                  ),
                ),
                IconButton(
                  icon: const Icon(Icons.send),
                  onPressed: _sendMessage,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}