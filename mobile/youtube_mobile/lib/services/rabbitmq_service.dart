import 'dart:convert';
import 'package:stomp_dart_client/stomp.dart';
import 'package:stomp_dart_client/stomp_config.dart';
import 'package:rxdart/rxdart.dart';

class ViewUpdated {
  final String videoId;
  final int viewCount;
  ViewUpdated(this.videoId, this.viewCount);
}

class RabbitMqService {
  final _controller = PublishSubject<ViewUpdated>();
  StompClient? _stompClient;

  Stream<ViewUpdated> get stream => _controller.stream;

  void connect() {
    _stompClient = StompClient(
      config: StompConfig(
        url: 'ws://localhost:15674/ws',
        onConnect: (frame) {
          print('[RabbitMQ] Connected');
          _stompClient?.subscribe(
            destination: '/exchange/YouTube.Application.Common.Requests.Video:SendIncrementViewMessage',
            callback: (frame) {
              print(frame.body);
              if (frame.body != null) {
                try {
                  final body = jsonDecode(frame.body!);

                  final msg = body['message'];
                  final videoId = msg['videoId'] as String;
                  final viewCount = msg['viewCount'] as int;

                  print(viewCount);

                  _controller.add(ViewUpdated(videoId, viewCount));
                } catch (e) {
                  print('[RabbitMQ] Ошибка парсинга: $e');
                }
              }
            },
          );
        },
        onWebSocketError: (dynamic error) => print('[RabbitMQ] WebSocket error: $error'),
        onStompError: (dynamic error) => print('[RabbitMQ] STOMP error: $error'),
        onDisconnect: (frame) => print('[RabbitMQ] Disconnected'),
      ),
    );

    _stompClient?.activate();
  }

  void dispose() {
    _stompClient?.deactivate();
    _controller.close();
  }
}
