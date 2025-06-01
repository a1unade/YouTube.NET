import { ChatSingleItem } from "../../../interfaces/chat/chat-single-item.ts";
import { ChatMessage } from "../../../interfaces/chat/chat-message.ts";
import ChatSingleMessage from "../components/chat-single-message.tsx";
import React, { useEffect, useRef, useState } from "react";
import apiClient from "../../../utils/apiClient.ts";
import { ChatHistoryResponse } from "../../../interfaces/chat/chat-history-response.ts";
import GrpcChatWindowInput from "./grpc-chat-window-input.tsx";

const formatDate = (date: Date) => {
  const options: Intl.DateTimeFormatOptions = {
    year: "numeric",
    month: "long",
    day: "numeric",
  };
  return date.toLocaleDateString(undefined, options);
};

const GrpcChatWindow = (props: {
  chat: ChatSingleItem | undefined;
  joinChat: (userId: string, chatId: string | null) => Promise<void>;
  userId: string | null;
  chatId: string | null;
  sendMessage: (
    chatId: string,
    userId: string,
    message: string,
  ) => Promise<void>;
  isConnected: boolean;
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
    isConnected,
  } = props;
  const [hasJoinedChat, setHasJoinedChat] = useState(false);
  const [pageCount, setPageCount] = useState(0);
  const [fetching, setFetching] = useState(true);
  const [page, setPage] = useState(1);
  const componentRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    if (chatId === null) {
      setFetching(true);
      setPage(1);
      setChatMessages([]);
    }
  }, [chatId]);

  useEffect(() => {
    console.log({ chatId, hasJoinedChat, userId, isConnected });
    const checkAndJoinChat = async () => {
      if (chatId !== null && !hasJoinedChat && userId !== null) {
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

          if (response.data.chatMessages !== null) {
            setChatMessages((prevData) => [
              ...prevData,
              ...response.data.chatMessages,
            ]);
          }
        })
        .catch((error) => {
          console.error("Error fetching data:", error);
        })
        .finally(() => setFetching(false));
    }
  }, [fetching, chatId, hasJoinedChat]);

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

  return chatId !== null ? (
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
          ? chatMessages.map((message: ChatMessage, index: number) => {
              const messageDate = new Date(message.date);
              const nextMessageDate =
                index < chatMessages.length - 1
                  ? new Date(chatMessages[index + 1].date)
                  : null;

              const isEndOfDay =
                !nextMessageDate ||
                nextMessageDate.toDateString() !== messageDate.toDateString();

              return (
                <React.Fragment key={index}>
                  <ChatSingleMessage message={message} userId={userId} />
                  {isEndOfDay && (
                    <div className="date-separator">
                      {formatDate(messageDate)}
                    </div>
                  )}
                </React.Fragment>
              );
            })
          : null}
      </div>
      <GrpcChatWindowInput
        userId={userId}
        chatId={chatId}
        sendMessage={sendMessage}
      />
    </div>
  ) : (
    <div className="chat-not-selected-layout">
      <span>Выберите кому хотели бы написать</span>
    </div>
  );
};

export default GrpcChatWindow;
