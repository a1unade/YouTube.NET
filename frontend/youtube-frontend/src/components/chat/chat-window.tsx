import ChatSingleMessage from './chat-single-message.tsx';
import ChatWindowInputSection from './chat-window-input-section.tsx';
import { ChatMessage } from '../../interfaces/chat/chat-message.ts';
import React, { useEffect } from 'react';
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

  useEffect(() => {
    if (active && chatId) {
      apiClient
        .get<ChatHistoryResponse>(`Chat/ChatMessagesByDay?Page=1&ChatId=${chatId}`)
        .then((response) => {
          setChatMessages(response.data.chatMessages);
        });
    }
  }, [active, chatId]);

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

  return (
    <div className="chat-selected-layout">
      <div className="chat-section-layout">
        {chatMessages !== null
          ? chatMessages.map((message: ChatMessage, index: number) => (
              <ChatSingleMessage key={index} message={message} userId={userId} />
            ))
          : null}
      </div>
      <ChatWindowInputSection userId={userId} chatId={chatId} sendMessage={sendMessage} />
    </div>
  );
};

export default ChatWindow;
