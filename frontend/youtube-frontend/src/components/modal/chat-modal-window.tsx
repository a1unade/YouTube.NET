import React, { useEffect, useState } from 'react';
import ChatWindow from '../chat/chat-window.tsx';
import { useSignalR } from '../../hooks/chat/use-signalr.ts';
import { ChatMessage } from '../../interfaces/chat/chat-message.ts';

const ChatModalWindow = (props: {
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
  userId: string | null;
}) => {
  const { active, setActive, userId } = props;
  const [chatId, setChatId] = useState<string | null>(null);
  const [chatMessages, setChatMessages] = useState<ChatMessage[]>([]);
  const [hasJoinedChat, setHasJoinedChat] = useState(false);

  const { joinChat, sendMessage, leaveChat, readMessages } = useSignalR({
    setChatId,
    setChatMessages,
  });

  useEffect(() => {
    if (active && !hasJoinedChat && userId) {
      joinChat(userId, chatId).then(() => {
        setHasJoinedChat(true);
      });
    } else if (!active) {
      setHasJoinedChat(false);
    }
  }, [active, userId, joinChat, chatId, hasJoinedChat]);

  const handleClose = () => {
    const modal = document.getElementById('chat-modal');
    modal!.classList.remove('chat-opened');
    leaveChat(chatId);
    setActive(false);
  };

  return (
    <div
      id="chat-modal"
      className={active ? 'chat-window-container chat-opened' : 'chat-window-container'}
    >
      <div className="chat-modal-header">
        <span>Чат с поддержкой</span>
        <svg
          onClick={handleClose}
          xmlns="http://www.w3.org/2000/svg"
          enableBackground="new 0 0 24 24"
          height="24"
          viewBox="0 0 24 24"
          width="24"
          focusable="false"
          aria-hidden="true"
        >
          <path d="m12.71 12 8.15 8.15-.71.71L12 12.71l-8.15 8.15-.71-.71L11.29 12 3.15 3.85l.71-.71L12 11.29l8.15-8.15.71.71L12.71 12z" />
        </svg>
      </div>
      <ChatWindow
        chatId={chatId}
        userId={userId}
        active={active}
        setChatMessages={setChatMessages}
        chatMessages={chatMessages}
        sendMessage={sendMessage}
        readMessages={readMessages}
      />
    </div>
  );
};

export default ChatModalWindow;
