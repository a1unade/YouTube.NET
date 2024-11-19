import React, { useEffect, useRef, useState } from 'react';

import ChatSingleMessage from './chat-single-message.tsx';
import ChatWindowInputSection from './chat-window-input-section.tsx';

import { ChatMessage } from '../../interfaces/chat/chat-message.ts';
import apiClient from '../../utils/apiClient.ts';
import { ChatHistoryResponse } from '../../interfaces/chat/chat-history-response.ts';

/**
 * Компонент окна чата, отображает историю чата и блок для отправки сообщений.
 *
 * Используется в модальном окне чата техподдержки. Компонент отвечает за
 * загрузку и отображение сообщений, а также за обработку отправки новых
 * сообщений и обновление статуса прочтения.
 *
 * @param {Object} props - Свойства компонента.
 * @param {boolean} props.active - Состояние модального окна чата: открыто (`true`) или закрыто (`false`).
 * @param {string | null} props.chatId - Идентификатор чата для загрузки его истории, входа в чат и отправки сообщений. Если `null`, чат не выбран.
 * @param {ChatMessage[]} props.chatMessages - Массив объектов, представляющих историю сообщений чата.
 * @param {React.Dispatch<React.SetStateAction<ChatMessage[]>>} props.setChatMessages - Функция для обновления состояния истории сообщений чата. Позволяет добавлять или изменять сообщения.
 * @param {(message: string, userId: string, chatId: string | null) => Promise<void>} props.sendMessage - Функция, отправляющая сообщение в чат. Возвращает `Promise`, которое исполнится после отправки.
 * @param {(messagesIds: string[], chatId: string | null) => Promise<void>} props.readMessages - Функция для обновления статуса прочтения сообщений.
 * @param {string | null} props.userId - Идентификатор пользователя. Используется для определения стилей сообщения (слева или справа).
 *
 * @returns {JSX.Element} Возвращает элемент интерфейса чата, содержащий историю сообщений и поле для ввода нового сообщения.
 *                      Возвращает `null`, если чат не выбран.
 *
 * @throws {Error} Выбрасывается при ошибках, связанных с запросами к API.
 *
 * @example Пример использования компонента:
 *   <ChatWindow
 *     active={true}
 *     chatId="123"
 *     chatMessages={[]}
 *     setChatMessages={(messages) => {}}
 *     sendMessage={async (msg, userId, chatId) => {}}
 *     readMessages={async (ids, chatId) => {}}
 *     userId="user1"
 *   />
 */

const ChatWindow = (props: {
  active: boolean;
  chatId: string | null;
  chatMessages: ChatMessage[];
  setChatMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
  sendMessage: (message: string, userId: string, chatId: string | null) => Promise<void>;
  readMessages: (messagesIds: string[], chatId: string | null) => Promise<void>;
  userId: string | null;
}): JSX.Element => {
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
