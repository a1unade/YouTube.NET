import React, { useState } from "react";
import { useGrpc } from "./use-grpc";
import { useSignalR } from "./use-signalr.ts";
import { ChatMessage } from "../../interfaces/chat/chat-message.ts";

interface UseChatTransportProps {
  setChatId: React.Dispatch<React.SetStateAction<string | null>>;
  setChatMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
}

export function useChatTransport({
  setChatMessages,
  setChatId,
}: UseChatTransportProps) {
  const grpc = useGrpc({ setChatMessages, setChatId });
  const signalr = useSignalR({ setChatMessages, setChatId });

  const [transportType, setTransportType] = useState<"grpc" | "signalr" | null>(
    null,
  );

  const joinChat = async (userId: string | null, chatId: string | null) => {
    if (!userId) throw new Error("UserId is required");

    try {
      await grpc.joinChat(chatId, userId);
      setTransportType("grpc");
    } catch (e) {
      console.warn("gRPC join failed, fallback to SignalR", e);
      try {
        await signalr.joinChat(userId, chatId);
        setTransportType("signalr");
      } catch (err) {
        console.error("SignalR join failed too", err);
        throw err;
      }
    }
  };

  const sendMessage = async (
    message: string,
    userId: string,
    chatId: string | null,
    fileId: string | null = null,
  ) => {
    if (transportType === "grpc") {
      return grpc.sendMessage(chatId!, userId, message);
    } else if (transportType === "signalr") {
      return signalr.sendMessage(message, userId, fileId, chatId);
    } else {
      throw new Error("No transport connected");
    }
  };

  const leaveChat = async () => {
    if (transportType === "grpc") {
      grpc.leaveChat();
    } else if (transportType === "signalr") {
      signalr.leaveChat(signalr.connectionRef.current?.connectionId || null);
    }
    setTransportType(null);
  };

  return {
    joinChat,
    sendMessage,
    leaveChat,
    isConnected:
      transportType === "grpc" ? grpc.isConnected : signalr.isConnected,
  };
}
