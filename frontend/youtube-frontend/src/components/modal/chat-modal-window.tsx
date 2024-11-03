import React from 'react';
import ChatWindow from '../chat/chat-window.tsx';

const ChatModalWindow = (props: {
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { active, setActive } = props;

  const handleClose = () => {
    const modal = document.getElementById('chat-modal');
    modal!.classList.remove('chat-opened');
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
      <ChatWindow />
    </div>
  );
};

export default ChatModalWindow;
