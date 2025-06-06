/**
 * @fileoverview gRPC-Web generated client stub for 
 * @enhanceable
 * @public
 */

// Code generated by protoc-gen-grpc-web. DO NOT EDIT.
// versions:
// 	protoc-gen-grpc-web v1.4.2
// 	protoc              v5.29.3
// source: chat.proto


/* eslint-disable */
// @ts-nocheck


import * as grpcWeb from 'grpc-web';

import * as chat_pb from './chat_pb.ts';


export class ChatServiceClient {
  client_: grpcWeb.AbstractClientBase;
  hostname_: string;
  credentials_: null | { [index: string]: string; };
  options_: null | { [index: string]: any; };

  constructor (hostname: string,
               credentials?: null | { [index: string]: string; },
               options?: null | { [index: string]: any; }) {
    if (!options) options = {};
    if (!credentials) credentials = {};
    options['format'] = 'text';

    this.client_ = new grpcWeb.GrpcWebClientBase(options);
    this.hostname_ = hostname.replace(/\/+$/, '');
    this.credentials_ = credentials;
    this.options_ = options;
  }

  methodDescriptorJoinChat = new grpcWeb.MethodDescriptor(
    '/ChatService/JoinChat',
    grpcWeb.MethodType.UNARY,
    chat_pb.JoinChatRequest,
    chat_pb.JoinChatResponse,
    (request: chat_pb.JoinChatRequest) => {
      return request.serializeBinary();
    },
    chat_pb.JoinChatResponse.deserializeBinary
  );

  joinChat(
    request: chat_pb.JoinChatRequest,
    metadata: grpcWeb.Metadata | null): Promise<chat_pb.JoinChatResponse>;

  joinChat(
    request: chat_pb.JoinChatRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: chat_pb.JoinChatResponse) => void): grpcWeb.ClientReadableStream<chat_pb.JoinChatResponse>;

  joinChat(
    request: chat_pb.JoinChatRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: chat_pb.JoinChatResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/ChatService/JoinChat',
        request,
        metadata || {},
        this.methodDescriptorJoinChat,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/ChatService/JoinChat',
    request,
    metadata || {},
    this.methodDescriptorJoinChat);
  }

  methodDescriptorSendMessage = new grpcWeb.MethodDescriptor(
    '/ChatService/SendMessage',
    grpcWeb.MethodType.UNARY,
    chat_pb.SendMessageRequest,
    chat_pb.SendMessageResponse,
    (request: chat_pb.SendMessageRequest) => {
      return request.serializeBinary();
    },
    chat_pb.SendMessageResponse.deserializeBinary
  );

  sendMessage(
    request: chat_pb.SendMessageRequest,
    metadata: grpcWeb.Metadata | null): Promise<chat_pb.SendMessageResponse>;

  sendMessage(
    request: chat_pb.SendMessageRequest,
    metadata: grpcWeb.Metadata | null,
    callback: (err: grpcWeb.RpcError,
               response: chat_pb.SendMessageResponse) => void): grpcWeb.ClientReadableStream<chat_pb.SendMessageResponse>;

  sendMessage(
    request: chat_pb.SendMessageRequest,
    metadata: grpcWeb.Metadata | null,
    callback?: (err: grpcWeb.RpcError,
               response: chat_pb.SendMessageResponse) => void) {
    if (callback !== undefined) {
      return this.client_.rpcCall(
        this.hostname_ +
          '/ChatService/SendMessage',
        request,
        metadata || {},
        this.methodDescriptorSendMessage,
        callback);
    }
    return this.client_.unaryCall(
    this.hostname_ +
      '/ChatService/SendMessage',
    request,
    metadata || {},
    this.methodDescriptorSendMessage);
  }

  methodDescriptorMessageStream = new grpcWeb.MethodDescriptor(
    '/ChatService/MessageStream',
    grpcWeb.MethodType.SERVER_STREAMING,
    chat_pb.JoinChatRequest,
    chat_pb.ChatMessageResponse,
    (request: chat_pb.JoinChatRequest) => {
      return request.serializeBinary();
    },
    chat_pb.ChatMessageResponse.deserializeBinary
  );

  messageStream(
    request: chat_pb.JoinChatRequest,
    metadata?: grpcWeb.Metadata): grpcWeb.ClientReadableStream<chat_pb.ChatMessageResponse> {
    return this.client_.serverStreaming(
      this.hostname_ +
        '/ChatService/MessageStream',
      request,
      metadata || {},
      this.methodDescriptorMessageStream);
  }

}

