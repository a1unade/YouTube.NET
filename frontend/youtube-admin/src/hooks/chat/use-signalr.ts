import React, { useRef, useEffect } from "react";
import {
  HubConnectionBuilder,
  HubConnection,
  HubConnectionState,
} from "@microsoft/signalr";
import { ChatMessage } from "../../interfaces/chat/chat-message.ts";
import { ChatMessageResponse } from "../../interfaces/chat/chat-message-response.ts";

interface ChatConnection {
  UserId: string;
  ChatId: string | null;
}

interface UseSignalRProps {
  setChatId: React.Dispatch<React.SetStateAction<string | null>>;
  setChatMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
}

export const useSignalR = ({ setChatId, setChatMessages }: UseSignalRProps) => {
  const connectionRef = useRef<HubConnection | null>(null);

  const joinChat = async (userId: string, chatId: string | null) => {
    if (
      !connectionRef.current ||
      connectionRef.current.state !== HubConnectionState.Connected
    ) {
      console.error("Connection not established");
      return;
    }

    const connection: ChatConnection = { UserId: userId, ChatId: chatId };

    try {
      const chatIdReturned = await connectionRef.current.invoke(
        "JoinChat",
        connection,
      );
      setChatId(chatIdReturned);
    } catch (error) {
      console.error("Error joining chat:", error);
    }
  };

  const sendMessage = async (
    message: string,
    userId: string,
    chatId: string | null,
  ) => {
    if (
      !connectionRef.current ||
      connectionRef.current.state !== HubConnectionState.Connected
    ) {
      console.error("Connection not established");
      return;
    }

    const request = { UserId: userId, ChatId: chatId, Message: message };

    try {
      await connectionRef.current.invoke("SendMessage", request);
    } catch (error) {
      console.error("Error sending message:", error);
    }
  };

  const startConnection = async () => {
    const newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:8080/supportChatHub")
      .withAutomaticReconnect()
      .build();

    connectionRef.current = newConnection;

    newConnection.on("ReceiveMessage", (message: ChatMessageResponse) => {
      const now = new Date();
      const hours = now.getHours().toString().padStart(2, "0");
      const minutes = now.getMinutes().toString().padStart(2, "0");
      const mes: ChatMessage = {
        messageId: "",
        message: message.message,
        senderId: message.userId,
        attachment: null,
        time: `${hours}:${minutes}`,
        isRead: false,
      };
      setChatMessages((prevMessages: ChatMessage[]) => [mes, ...prevMessages]);
    });

    try {
      await newConnection.start();
    } catch (error) {
      console.error("Error establishing connection:", error);
    }
  };

  const leaveChat = async (chatId: string | null) => {
    if (
      !connectionRef.current ||
      connectionRef.current.state !== HubConnectionState.Connected
    ) {
      console.error("Connection not established");
      return;
    }

    try {
      await connectionRef.current.invoke("LeaveChat", chatId);

      await connectionRef.current.stop();

      setChatId(null);
      setChatMessages([]);
    } catch (error) {
      console.error("Error leaving chat:", error);
    }
  };

  useEffect(() => {
    startConnection();

    return () => {
      connectionRef.current?.stop();
    };
  }, []);

  return { joinChat, sendMessage, connectionRef, leaveChat, startConnection };
};
