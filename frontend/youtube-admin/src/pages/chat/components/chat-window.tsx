import { ChatSingleItem } from "../../../interfaces/chat/chat-single-item.ts";
import { ChatMessage } from "../../../interfaces/chat/chat-message.ts";
import ChatSingleMessage from "./chat-single-message.tsx";
import ChatWindowInputSection from "./chat-window-input-section.tsx";
import React, { useEffect, useRef, useState } from "react";
import apiClient from "../../../utils/apiClient.ts";
import { ChatHistoryResponse } from "../../../interfaces/chat/chat-history-response.ts";

const ChatWindow = (props: {
  chat: ChatSingleItem | undefined;
  joinChat: (userId: string, chatId: string | null) => Promise<void>;
  userId: string | null;
  chatId: string | null;
  sendMessage: (
    message: string,
    userId: string,
    chatId: string | null,
  ) => Promise<void>;
  isConnected: boolean;
  chatMessages: ChatMessage[];
  setChatMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
  readMessages: (messagesIds: string[], chatId: string) => Promise<void>;
}) => {
  const {
    chat,
    joinChat,
    setChatMessages,
    sendMessage,
    userId,
    chatId,
    chatMessages,
    readMessages,
    isConnected,
  } = props;
  const [hasJoinedChat, setHasJoinedChat] = useState(false);
  const [pageCount, setPageCount] = useState(0);
  const [fetching, setFetching] = useState(true);
  const [page, setPage] = useState(1);
  const componentRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    const checkAndJoinChat = async () => {
      if (chatId !== null && !hasJoinedChat && userId !== null && isConnected) {
        joinChat(userId, chatId).then(() => {
          setHasJoinedChat(true);
        });
      } else if (!chatId) {
        setHasJoinedChat(false);
      }
    };

    checkAndJoinChat();
  }, [chatId, joinChat, hasJoinedChat, userId, isConnected]);

  useEffect(() => {
    if (fetching && chatId && hasJoinedChat) {
      apiClient
        .get<ChatHistoryResponse>(
          `Chat/ChatMessagesByDay?Page=${page}&ChatId=${chatId}`,
        )
        .then((response) => {
          if (pageCount === 0) {
            setPageCount(response.data.pageCount);
          }
          setChatMessages((prevData) => [
            ...prevData,
            ...response.data.chatMessages,
          ]);
        })
        .catch((error) => {
          console.error("Error fetching data:", error);
        })
        .finally(() => setFetching(false));
    }
  }, [fetching, chatId, hasJoinedChat]);

  useEffect(() => {
    const updateUnreadMessages = async () => {
      if (chatMessages !== null && chatId !== null) {
        const unreadMessagesId = chatMessages
          .filter(
            (message) =>
              !message.isRead &&
              message.senderId !== userId &&
              message.messageId !== "",
          )
          .map((message) => message.messageId);

        if (unreadMessagesId.length > 0) {
          try {
            await readMessages(unreadMessagesId, chatId);
          } catch (error) {
            console.error("Error marking messages as read:", error);
          }
        }
      }
    };

    updateUnreadMessages();
  }, [chatMessages, userId, chatId]);

  useEffect(() => {
    const currentRef = componentRef.current;
    if (currentRef) {
      currentRef.addEventListener("scroll", handleScroll);
    }

    return () => {
      if (currentRef) {
        currentRef.removeEventListener("scroll", handleScroll);
      }
    };
  }, [chatMessages, pageCount, setFetching]);

  const handleScroll = (event: Event) => {
    const target = event.target as HTMLElement;

    if (target.scrollTop < 600 && page < pageCount) {
      setFetching(true);
      setPage(page + 1);
    }
  };

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
      <div className="chat-section-layout" ref={componentRef}>
        {chatMessages !== null
          ? chatMessages.map((message: ChatMessage, index: number) => (
              <ChatSingleMessage
                key={index}
                message={message}
                userId={userId}
              />
            ))
          : null}
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
