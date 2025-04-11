import { ChatSingleItem } from "../../../interfaces/chat/chat-single-item.ts";
import { ChatMessage } from "../../../interfaces/chat/chat-message.ts";
import ChatSingleMessage from "./chat-single-message.tsx";
import ChatWindowInputSection from "./chat-window-input-section.tsx";
import React, { useEffect, useRef, useState } from "react";
import apiClient from "../../../utils/apiClient.ts";
import { ChatHistoryResponse } from "../../../interfaces/chat/chat-history-response.ts";
import ChatConfirmAttachmentModal from "../../../components/modal/chat-confirm-attachment-modal.tsx";
import ChatAddedAttachment from "./chat-added-attachment.tsx";

const formatDate = (date: Date) => {
  const options: Intl.DateTimeFormatOptions = {
    year: "numeric",
    month: "long",
    day: "numeric",
  };
  return date.toLocaleDateString(undefined, options);
};

const ChatWindow = (props: {
  chat: ChatSingleItem | undefined;
  joinChat: (userId: string, chatId: string | null) => Promise<void>;
  userId: string | null;
  chatId: string | null;
  sendMessage: (
    message: string,
    userId: string,
    fileId: string | null,
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
  const [hasAttachment, setHasAttachment] = useState(false);
  const [fileIsLoading, setFileIsLoading] = useState(false);
  const [file, setFile] = useState<File | null>(null);
  const [confirmIsActive, setConfirmIsActive] = useState(false);
  const [shouldSendFile, setShouldSendFile] = useState(false);

  useEffect(() => {
    if (chatId === null) {
      setFetching(true);
      setPage(1);
      setChatMessages([]);
    }
  }, [chatId]);

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

  return chatId !== null ? (
    <div className="chat-selected-layout">
      <div
        className="chat-single-item-layout"
        style={{
          height: hasAttachment ? "calc(100vh - 400px)" : "calc(100vh - 140px)",
        }}
      >
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
      <div
        className={`chat-section-layout ${hasAttachment ? "with-attachment" : ""}`}
        ref={componentRef}
      >
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
      {hasAttachment && (
        <ChatAddedAttachment
          file={file}
          setFile={setFile}
          isLoading={fileIsLoading}
          setHasAttachment={setHasAttachment}
        />
      )}
      <ChatWindowInputSection
        userId={userId}
        chatId={chatId}
        sendMessage={sendMessage}
        setFile={setFile}
        setConfirmIsActive={setConfirmIsActive}
        setShouldSendFile={setShouldSendFile}
        shouldSendFile={shouldSendFile}
        file={file}
        setLoading={setFileIsLoading}
        setHasAttachment={setHasAttachment}
      />
      <ChatConfirmAttachmentModal
        file={file!}
        active={confirmIsActive}
        setActive={setConfirmIsActive}
        setShouldSendFile={setShouldSendFile}
        setFile={setFile}
        setHasAttachment={setHasAttachment}
        setFileIsLoading={setFileIsLoading}
      />
    </div>
  ) : (
    <div className="chat-not-selected-layout">
      <span>Выберите кому хотели бы написать</span>
    </div>
  );
};

export default ChatWindow;
