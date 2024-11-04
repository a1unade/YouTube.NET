import ChatSingleItem from "./components/chat-single-item.tsx";
import ChatWindow from "./components/chat-window.tsx";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ChatMessage } from "../../interfaces/chat/chat-message.ts";
import { useSignalR } from "../../hooks/chat/use-signalr.ts";
import apiClient from "../../utils/apiClient.ts";
import { ChatCollectionResponse } from "../../interfaces/chat/chat-collection-response.ts";

const ChatPage = () => {
  const { id } = useParams<{ id: string }>();
  const [userId] = useState("4bbb278f-e578-4f79-9a37-1f395f06b785");
  const [chatId, setChatId] = useState<string | null>(id || null);
  const [chatMessages, setChatMessages] = useState<ChatMessage[]>([]);
  const [chatList, setChatList] = useState<ChatSingleItem[]>([]);

  const navigate = useNavigate();
  const { joinChat, sendMessage, leaveChat } = useSignalR({
    setChatId,
    setChatMessages,
  });

  useEffect(() => {
    apiClient
      .get<ChatCollectionResponse>(
        "Chat/GetHistoryCollectionForCard?Page=1&Size=1",
      )
      .then((response) => {
        setChatList(response.data.chatCardDtos);
      });
  }, []);

  const handleKeyDown = (event: KeyboardEvent) => {
    if (event.key === "Escape") {
      setChatId(null);
      navigate("/chat");
      leaveChat(chatId);
    }
  };

  useEffect(() => {
    window.addEventListener("keydown", handleKeyDown);

    return () => {
      window.removeEventListener("keydown", handleKeyDown);
    };
  }, []);

  return (
    <div className="chat-page-layout">
      <div className="chat-list-section">
        <div>
          <div className="chat-list-section-header">
            <span>Чаты</span>
          </div>
          {chatList.map((chat: ChatSingleItem) => (
            <ChatSingleItem
              selected={chatId}
              setSelected={setChatId}
              key={chat.chatId}
              chat={chat}
            />
          ))}
        </div>
      </div>
      <ChatWindow
        chatMessages={chatMessages}
        setChatMessages={setChatMessages}
        chatId={chatId}
        userId={userId}
        joinChat={joinChat}
        sendMessage={sendMessage}
        chat={chatList.find((chat) => chat.chatId === chatId)}
      />
    </div>
  );
};

export default ChatPage;
