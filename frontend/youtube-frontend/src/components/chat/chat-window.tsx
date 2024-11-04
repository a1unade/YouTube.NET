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
  userId: string;
}) => {
  const { chatId, active, userId, chatMessages, setChatMessages, sendMessage } = props;

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
      const unreadMessagesId = chatMessages
        .filter((message) => !message.isRead && message.senderId !== userId)
        .map((message) => message.messageId);
      console.log(unreadMessagesId);

      if (unreadMessagesId.length > 0) {
        try {
          await apiClient.patch('Chat/ReadMessages', { messagesId: unreadMessagesId }).then(() => {
            setChatMessages((prevMessages) =>
              prevMessages.map((msg) =>
                unreadMessagesId.includes(msg.messageId) ? { ...msg, isRead: true } : msg,
              ),
            );
          });
        } catch (error) {
          console.error('Error marking messages as read:', error);
        }
      }
    };

    updateUnreadMessages();
  }, [chatMessages, userId]);

  return (
    <div className="chat-selected-layout">
      <div className="chat-section-layout">
        {chatMessages.map((message: ChatMessage, index: number) => (
          <ChatSingleMessage key={index} message={message} userId={userId} />
        ))}
      </div>
      <ChatWindowInputSection userId={userId} chatId={chatId} sendMessage={sendMessage} />
    </div>
  );
};

export default ChatWindow;
