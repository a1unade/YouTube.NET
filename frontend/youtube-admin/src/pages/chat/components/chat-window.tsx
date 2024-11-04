import { ChatSingleItem } from "../../../interfaces/chat/chat-single-item.ts";
import { ChatMessage } from "../../../interfaces/chat/chat-message.ts";
import ChatSingleMessage from "./chat-single-message.tsx";
import ChatWindowInputSection from "./chat-window-input-section.tsx";
import React, { useEffect, useState } from "react";
import apiClient from "../../../utils/apiClient.ts";
import { ChatHistoryResponse } from "../../../interfaces/chat/chat-history-response.ts";

const ChatWindow = (props: {
  chat: ChatSingleItem | undefined;
  joinChat: (userId: string, chatId: string | null) => Promise<void>;
  userId: string;
  chatId: string | null;
  sendMessage: (
    message: string,
    userId: string,
    chatId: string | null,
  ) => Promise<void>;
  chatMessages: ChatMessage[];
  setChatMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
}) => {
  const {
    chat,
    joinChat,
    setChatMessages,
    sendMessage,
    userId,
    chatId,
    chatMessages,
  } = props;
  const [hasJoinedChat, setHasJoinedChat] = useState(false);

  useEffect(() => {
    if (chatId !== null && !hasJoinedChat) {
      joinChat(userId, chatId).then(() => {
        setHasJoinedChat(true);
      });
    } else if (!chat) {
      setHasJoinedChat(false);
    }
  }, [chatId, joinChat, hasJoinedChat]);

  useEffect(() => {
    if (chatId && hasJoinedChat) {
      apiClient
        .get<ChatHistoryResponse>(
          `Chat/ChatMessagesByDay?Page=1&ChatId=${chatId}`,
        )
        .then((response) => {
          setChatMessages(response.data.chatMessages);
        });
    }
  }, [chatId, hasJoinedChat]);

  useEffect(() => {
    const updateUnreadMessages = async () => {
      const unreadMessagesId = chatMessages
        .filter((message) => !message.isRead && message.senderId !== userId)
        .map((message) => message.messageId);
      console.log(unreadMessagesId);

      if (unreadMessagesId.length > 0) {
        try {
          await apiClient
            .patch("Chat/ReadMessages", { messagesId: unreadMessagesId })
            .then(() => {
              setChatMessages((prevMessages) =>
                prevMessages.map((msg) =>
                  unreadMessagesId.includes(msg.messageId)
                    ? { ...msg, is_read: true }
                    : msg,
                ),
              );
            });
        } catch (error) {
          console.error("Error marking messages as read:", error);
        }
      }
    };

    updateUnreadMessages();
  }, [chatMessages, userId]);

  return chatId ? (
    <div className="chat-selected-layout">
      <div className="chat-single-item-layout">
        <img
          src={
            chat?.avatarUrl ||
            "https://avatars.githubusercontent.com/u/113981832?v=4"
          }
          className="circular-avatar"
          alt=""
        />
        <p>{chat?.userName}</p>
      </div>
      <div className="chat-section-layout">
        {chatMessages.map((message: ChatMessage, index: number) => (
          <ChatSingleMessage key={index} message={message} userId={userId} />
        ))}
      </div>
      <ChatWindowInputSection
        sendMessage={sendMessage}
        chatId={chatId}
        userId={userId}
      />
    </div>
  ) : (
    <div className="chat-not-selected-layout">
      <span>Выберите кому хотели бы написать</span>
    </div>
  );
};

export default ChatWindow;
