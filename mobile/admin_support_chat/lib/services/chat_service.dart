import 'dart:async';
import 'package:grpc/grpc_web.dart';
import 'package:admin_support_chat/src/generated/chat.pbgrpc.dart';

class ChatGrpcClient {
  late final ChatServiceClient _client;
  final GrpcWebClientChannel _channel;

  ChatGrpcClient(String host, int port)
      : _channel = GrpcWebClientChannel.xhr(
          Uri.parse('http://$host:$port'),
        ) {
    _client = ChatServiceClient(_channel);
  }

  Future<JoinChatResponse> joinChat(String userId, [String? chatId]) async {
    final request = JoinChatRequest()
      ..userId = userId
      ..chatId = chatId ?? '';
    return await _client.joinChat(request);
  }

  Future<SendMessageResponse> sendMessage(String chatId, String userId, String message) async {
    final request = SendMessageRequest()
      ..chatId = chatId
      ..userId = userId
      ..message = message;
    return await _client.sendMessage(request);
  }

  Stream<ChatMessageResponse> messageStream(String chatId, String userId) {
    final request = JoinChatRequest()
      ..chatId = chatId
      ..userId = userId;
    return _client.messageStream(request);
  }

  Future<void> shutdown() async => _channel.shutdown();
}