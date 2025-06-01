//
//  Generated code. Do not modify.
//  source: chat.proto
//
// @dart = 3.3

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names
// ignore_for_file: curly_braces_in_flow_control_structures
// ignore_for_file: deprecated_member_use_from_same_package, library_prefixes
// ignore_for_file: non_constant_identifier_names

import 'dart:async' as $async;
import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'package:protobuf/protobuf.dart' as $pb;

import 'chat.pb.dart' as $0;

export 'chat.pb.dart';

@$pb.GrpcServiceName('ChatService')
class ChatServiceClient extends $grpc.Client {
  /// The hostname for this service.
  static const $core.String defaultHost = '';

  /// OAuth scopes needed for the client.
  static const $core.List<$core.String> oauthScopes = [
    '',
  ];

  static final _$joinChat = $grpc.ClientMethod<$0.JoinChatRequest, $0.JoinChatResponse>(
      '/ChatService/JoinChat',
      ($0.JoinChatRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.JoinChatResponse.fromBuffer(value));
  static final _$sendMessage = $grpc.ClientMethod<$0.SendMessageRequest, $0.SendMessageResponse>(
      '/ChatService/SendMessage',
      ($0.SendMessageRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.SendMessageResponse.fromBuffer(value));
  static final _$messageStream = $grpc.ClientMethod<$0.JoinChatRequest, $0.ChatMessageResponse>(
      '/ChatService/MessageStream',
      ($0.JoinChatRequest value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.ChatMessageResponse.fromBuffer(value));

  ChatServiceClient(super.channel, {super.options, super.interceptors});

  $grpc.ResponseFuture<$0.JoinChatResponse> joinChat($0.JoinChatRequest request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$joinChat, request, options: options);
  }

  $grpc.ResponseFuture<$0.SendMessageResponse> sendMessage($0.SendMessageRequest request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$sendMessage, request, options: options);
  }

  $grpc.ResponseStream<$0.ChatMessageResponse> messageStream($0.JoinChatRequest request, {$grpc.CallOptions? options}) {
    return $createStreamingCall(_$messageStream, $async.Stream.fromIterable([request]), options: options);
  }
}

@$pb.GrpcServiceName('ChatService')
abstract class ChatServiceBase extends $grpc.Service {
  $core.String get $name => 'ChatService';

  ChatServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.JoinChatRequest, $0.JoinChatResponse>(
        'JoinChat',
        joinChat_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.JoinChatRequest.fromBuffer(value),
        ($0.JoinChatResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.SendMessageRequest, $0.SendMessageResponse>(
        'SendMessage',
        sendMessage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.SendMessageRequest.fromBuffer(value),
        ($0.SendMessageResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.JoinChatRequest, $0.ChatMessageResponse>(
        'MessageStream',
        messageStream_Pre,
        false,
        true,
        ($core.List<$core.int> value) => $0.JoinChatRequest.fromBuffer(value),
        ($0.ChatMessageResponse value) => value.writeToBuffer()));
  }

  $async.Future<$0.JoinChatResponse> joinChat_Pre($grpc.ServiceCall $call, $async.Future<$0.JoinChatRequest> $request) async {
    return joinChat($call, await $request);
  }

  $async.Future<$0.SendMessageResponse> sendMessage_Pre($grpc.ServiceCall $call, $async.Future<$0.SendMessageRequest> $request) async {
    return sendMessage($call, await $request);
  }

  $async.Stream<$0.ChatMessageResponse> messageStream_Pre($grpc.ServiceCall $call, $async.Future<$0.JoinChatRequest> $request) async* {
    yield* messageStream($call, await $request);
  }

  $async.Future<$0.JoinChatResponse> joinChat($grpc.ServiceCall call, $0.JoinChatRequest request);
  $async.Future<$0.SendMessageResponse> sendMessage($grpc.ServiceCall call, $0.SendMessageRequest request);
  $async.Stream<$0.ChatMessageResponse> messageStream($grpc.ServiceCall call, $0.JoinChatRequest request);
}
