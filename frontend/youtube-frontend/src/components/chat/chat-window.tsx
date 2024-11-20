import React, { useEffect, useRef, useState } from 'react';

import ChatSingleMessage from './chat-single-message.tsx';
import ChatWindowInputSection from './chat-window-input-section.tsx';

import { ChatMessage } from '../../interfaces/chat/chat-message.ts';
import apiClient from '../../utils/apiClient.ts';
import { ChatHistoryResponse } from '../../interfaces/chat/chat-history-response.ts';

const ChatWindow = (props: {
  active: boolean;
  chatId: string | null;
  chatMessages: ChatMessage[];
  setChatMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
  sendMessage: (message: string, userId: string, chatId: string | null) => Promise<void>;
  readMessages: (messagesIds: string[], chatId: string | null) => Promise<void>;
  userId: string | null;
}) => {
  const { chatId, active, userId, readMessages, chatMessages, setChatMessages, sendMessage } =
    props;

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
    if (fetching && chatId && active) {
      apiClient
        .get<ChatHistoryResponse>(`Chat/ChatMessagesByDay?Page=${page}&ChatId=${chatId}`)
        .then((response) => {
          if (pageCount === 0) {
            setPageCount(response.data.pageCount);
          }

          if (response.data.chatMessages !== null) {
            setChatMessages((prevData) => [...prevData, ...response.data.chatMessages]);
          }
        })
        .catch((error) => {
          console.error('Error fetching data:', error);
        })
        .finally(() => setFetching(false));
    }
  }, [fetching, chatId, active]);

  useEffect(() => {
    const updateUnreadMessages = async () => {
      if (chatMessages && chatId) {
        const unreadMessagesId = chatMessages
          .filter(
            (message) => !message.isRead && message.senderId !== userId && message.messageId !== '',
          )
          .map((message) => message.messageId);

        if (unreadMessagesId.length > 0) {
          try {
            await readMessages(unreadMessagesId, chatId);
          } catch (error) {
            console.error('Error marking messages as read:', error);
          }
        }
      }
    };

    updateUnreadMessages();
  }, [chatMessages, userId, chatId]);

  useEffect(() => {
    const currentRef = componentRef.current;
    if (currentRef) {
      currentRef.addEventListener('scroll', handleScroll);
    }

    return () => {
      if (currentRef) {
        currentRef.removeEventListener('scroll', handleScroll);
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

  const formatDate = (date: Date) => {
    const options: Intl.DateTimeFormatOptions = {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    };
    return date.toLocaleDateString(undefined, options);
  };

  return (
    <div className="chat-selected-layout">
      <div className="chat-section-layout" ref={componentRef}>
        {chatMessages !== null
          ? chatMessages.map((message: ChatMessage, index: number) => {
              const messageDate = new Date(message.date);
              const nextMessageDate =
                index < chatMessages.length - 1 ? new Date(chatMessages[index + 1].date) : null;

              const isEndOfDay =
                !nextMessageDate || nextMessageDate.toDateString() !== messageDate.toDateString();

              return (
                <React.Fragment key={index}>
                  <ChatSingleMessage message={message} userId={userId} />
                  {isEndOfDay && <div className="date-separator">{formatDate(messageDate)}</div>}
                </React.Fragment>
              );
            })
          : null}
      </div>
      <ChatWindowInputSection userId={userId} chatId={chatId} sendMessage={sendMessage} />
    </div>
  );
};

export default ChatWindow;
