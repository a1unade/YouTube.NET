import * as jspb from 'google-protobuf'



export class JoinChatRequest extends jspb.Message {
  getUserId(): string;
  setUserId(value: string): JoinChatRequest;

  getChatId(): string;
  setChatId(value: string): JoinChatRequest;
  hasChatId(): boolean;
  clearChatId(): JoinChatRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): JoinChatRequest.AsObject;
  static toObject(includeInstance: boolean, msg: JoinChatRequest): JoinChatRequest.AsObject;
  static serializeBinaryToWriter(message: JoinChatRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): JoinChatRequest;
  static deserializeBinaryFromReader(message: JoinChatRequest, reader: jspb.BinaryReader): JoinChatRequest;
}

export namespace JoinChatRequest {
  export type AsObject = {
    userId: string,
    chatId?: string,
  }

  export enum ChatIdCase { 
    _CHAT_ID_NOT_SET = 0,
    CHAT_ID = 2,
  }
}

export class JoinChatResponse extends jspb.Message {
  getUserId(): string;
  setUserId(value: string): JoinChatResponse;

  getChatId(): string;
  setChatId(value: string): JoinChatResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): JoinChatResponse.AsObject;
  static toObject(includeInstance: boolean, msg: JoinChatResponse): JoinChatResponse.AsObject;
  static serializeBinaryToWriter(message: JoinChatResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): JoinChatResponse;
  static deserializeBinaryFromReader(message: JoinChatResponse, reader: jspb.BinaryReader): JoinChatResponse;
}

export namespace JoinChatResponse {
  export type AsObject = {
    userId: string,
    chatId: string,
  }
}

export class SendMessageRequest extends jspb.Message {
  getChatId(): string;
  setChatId(value: string): SendMessageRequest;

  getUserId(): string;
  setUserId(value: string): SendMessageRequest;

  getMessage(): string;
  setMessage(value: string): SendMessageRequest;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SendMessageRequest.AsObject;
  static toObject(includeInstance: boolean, msg: SendMessageRequest): SendMessageRequest.AsObject;
  static serializeBinaryToWriter(message: SendMessageRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SendMessageRequest;
  static deserializeBinaryFromReader(message: SendMessageRequest, reader: jspb.BinaryReader): SendMessageRequest;
}

export namespace SendMessageRequest {
  export type AsObject = {
    chatId: string,
    userId: string,
    message: string,
  }
}

export class SendMessageResponse extends jspb.Message {
  getSuccess(): boolean;
  setSuccess(value: boolean): SendMessageResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SendMessageResponse.AsObject;
  static toObject(includeInstance: boolean, msg: SendMessageResponse): SendMessageResponse.AsObject;
  static serializeBinaryToWriter(message: SendMessageResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SendMessageResponse;
  static deserializeBinaryFromReader(message: SendMessageResponse, reader: jspb.BinaryReader): SendMessageResponse;
}

export namespace SendMessageResponse {
  export type AsObject = {
    success: boolean,
  }
}

export class ChatMessageResponse extends jspb.Message {
  getMessageId(): string;
  setMessageId(value: string): ChatMessageResponse;

  getUserId(): string;
  setUserId(value: string): ChatMessageResponse;

  getMessage(): string;
  setMessage(value: string): ChatMessageResponse;

  getDate(): string;
  setDate(value: string): ChatMessageResponse;

  getTime(): string;
  setTime(value: string): ChatMessageResponse;

  getIsRead(): boolean;
  setIsRead(value: boolean): ChatMessageResponse;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ChatMessageResponse.AsObject;
  static toObject(includeInstance: boolean, msg: ChatMessageResponse): ChatMessageResponse.AsObject;
  static serializeBinaryToWriter(message: ChatMessageResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ChatMessageResponse;
  static deserializeBinaryFromReader(message: ChatMessageResponse, reader: jspb.BinaryReader): ChatMessageResponse;
}

export namespace ChatMessageResponse {
  export type AsObject = {
    messageId: string,
    userId: string,
    message: string,
    date: string,
    time: string,
    isRead: boolean,
  }
}

