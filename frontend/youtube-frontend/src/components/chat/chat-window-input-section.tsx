import { useEffect, useState } from 'react';

/**
 * Компонент секции ввода сообщений чата.
 *
 * Отвечает за ввод текста сообщения и его отправку в чат.
 * Позволяет пользователю вводить текст, который затем отправляется
 * с использованием переданной функции отправки сообщений.
 *
 * @param {Object} props - Свойства компонента.
 * @param {string | null} props.chatId - Идентификатор чата, в который будет отправлено сообщение. Если `null`, сообщение не будет отправлено.
 * @param {string | null} props.userId - Идентификатор пользователя, который отправляет сообщение.
 * @param {(message: string, userId: string, chatId: string | null) => Promise<void>} props.sendMessage - Функция для отправки сообщения в чат.
 *
 * @returns {JSX.Element} Возвращает элемент интерфейса секции ввода сообщения, содержащий текстовое поле и кнопку отправки.
 *
 * @example Пример использования компонента:
 *   <ChatWindowInputSection
 *     chatId="123"
 *     userId="user1"
 *     sendMessage={async (msg, userId, chatId) => {  реализация отправки сообщения  }
 *     />
 */

const ChatWindowInputSection = (props: {
  chatId: string | null;
  userId: string | null;
  sendMessage: (message: string, userId: string, chatId: string | null) => Promise<void>;
}): JSX.Element => {
  const { chatId, userId, sendMessage } = props;
  const [messageText, setMessageText] = useState('');

  useEffect(() => {
    const messageInput = document.getElementById('messageInput') as HTMLTextAreaElement;
    const sendButton = document.getElementById('sendButton') as HTMLDivElement;

    messageInput.addEventListener('input', () => {
      if (messageInput.value !== '' || messageText !== '') {
        sendButton.style.display = 'flex';
      } else {
        sendButton.style.display = 'none';
      }
    });
  }, [messageText]);

  const handleSendButtonClick = () => {
    setMessageText('');
    sendMessage(messageText, userId!, chatId);
  };

  return (
    <div className="input-section-layout">
      <textarea
        id="messageInput"
        value={messageText}
        onChange={(e) => setMessageText(e.target.value)}
        className="input-section-textarea"
        rows={4}
        maxLength={100}
        placeholder="Сообщение..."
      />
      <div
        className="input-section-send-message-button"
        id="sendButton"
        onClick={handleSendButtonClick}
      >
        <div className="input-section-attachment">
          <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" stroke="#000000">
            <g id="SVGRepo_bgCarrier" strokeWidth="0" />
            <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round" />
            <g id="SVGRepo_iconCarrier">
              <path
                d="M12 5V19M12 5L6 11M12 5L18 11"
                stroke="lightgray"
                strokeWidth="1.2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </g>
          </svg>
        </div>
      </div>
    </div>
  );
};

export default ChatWindowInputSection;
