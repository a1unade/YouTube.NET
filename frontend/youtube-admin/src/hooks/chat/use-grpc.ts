import React, { useState } from "react";
import { ChatServiceClient } from "../../generated/ChatServiceClientPb";
import type {
  JoinChatRequest,
  SendMessageRequest,
  ChatMessageResponse,
} from "../../generated/chat_pb"
import { ChatMessage } from "../../interfaces/chat/chat-message.ts";
import { ClientReadableStream } from "grpc-web";

interface UseGrpcProps {
  setChatId: React.Dispatch<React.SetStateAction<string | null>>;
  setChatMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
}

export function useGrpc({ setChatMessages, setChatId }: UseGrpcProps) {
  const [client] = useState(
    () => new ChatServiceClient("http://localhost:8081/"),
  );
  const [isConnected, setIsConnected] = useState(false);
  const [chatStream, setChatStream] =
    useState<ClientReadableStream<ChatMessageResponse> | null>(null);

  const joinChat = (chatId: string | null, userId: string | null) => {
    return new Promise<void>((resolve, reject) => {
      if (!userId) {
        reject("No userId");
        return;
      }

      const request = new JoinChatRequest();
      console.log("JoinChatRequest:", JoinChatRequest);

      console.log("typeof request.setChatId:", typeof request.setUserId);
      request.setUserId(userId);
      if (chatId) request.setChatId(chatId);

      try {
        const stream = client.messageStream(request, {});
        setChatStream(stream);

        stream.on("data", (message: ChatMessageResponse) => {
          setChatMessages((prev) => [
            ...prev,
            {
              messageId: message.getMessageId(),
              userId: message.getUserId(),
              message: message.getMessage(),
              date: message.getDate(),
              time: message.getTime(),
              isRead: message.getIsRead(),
            },
          ]);
        });
        stream.on("end", () => {
          setIsConnected(false);
        });
        stream.on("error", (err) => {
          setIsConnected(false);
          reject(err);
        });

        setIsConnected(true);
        setChatId(chatId);
        resolve();
      } catch (e) {
        reject(e);
      }
    });
  };

  const sendMessage = (chatId: string, userId: string, message: string) => {
    return new Promise<void>((resolve, reject) => {
      console.log("SendMessageRequest class:", SendMessageRequest);
      const request = new SendMessageRequest();
      console.log("request instance:", request);
      console.log("typeof request.setChatId:", typeof request.setChatId);
      request.setChatId(chatId);
      request.setUserId(userId);
      request.setMessage(message);

      client.sendMessage(request, {}, (err) => {
        if (err) {
          reject(err);
        } else {
          resolve();
        }
      });
    });
  };

  const leaveChat = () => {
    if (chatStream) {
      chatStream.cancel();
      setChatStream(null);
      setIsConnected(false);
    }
  };

  return {
    joinChat,
    sendMessage,
    leaveChat,
    isConnected,
  };
}
