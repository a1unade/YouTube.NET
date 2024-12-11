import { ChatMessage } from '../../interfaces/chat/chat-message.ts';
import ChatAttachmentImageModal from '../modal/chat-image-attachment-modal.tsx';
import { useState } from 'react';

/**
 * Компонент сообщения в чате.
 *
 * Добавлена возможность использования вложений у сообщения (картинка, файл).
 *
 * Используется только в чате техподдержки, стили для авторов сообщений подставляются автоматически.
 *
 * @param props
 * @param props.message {ChatMessage} Данные о сообщении.
 * @param props.userId {string | null} Идентификатор пользователя, для определения стилей у сообщения (должно находиться слева или справа, в зависимости от автора).
 *
 * @example Добавление сообщения со стороны админа.
 *   <ChatSingleMessage message={message} userId={"Admin ID"}>
 *
 * @example Добавление сообщения со стороны пользователя.
 *   <ChatSingleMessage message={message} userId={"User ID"}>
 *
 * @returns {null | JSX.Element}
 *
 * {null} - В случае, если идентификатор пользователя не определён.
 * {JSX.Element} - В случае, если идентификатор пользователя был получен и диалог с админом начался.
 */

const ChatSingleMessage = (props: {
  message: ChatMessage;
  userId: string | null;
}): JSX.Element | null => {
  const { message, userId } = props;
  const [isModalOpen, setModalOpen] = useState(false);

  if (!userId) return null;

  return (
    <>
      <div className="chat-message-layout">
        <div
          className={
            message.senderId === userId
              ? 'chat-single-message admin-message'
              : 'chat-single-message user-message'
          }
        >
          {(() => {
            switch (message.attachment?.type) {
              case 'image':
                return (
                  <>
                    <img
                      style={{ cursor: 'pointer' }}
                      src={message.attachment.link}
                      alt="attachment"
                      onClick={() => setModalOpen(true)}
                    />
                  </>
                );
              case 'file':
                return (
                  <a href={message.attachment.link} download>
                    <div style={{ width: 'fit-content', height: 'fit-content' }}>
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        x="0px"
                        y="0px"
                        width="27"
                        height="27"
                        viewBox="0,0,300,150"
                      >
                        <g
                          fill={message.senderId !== userId ? '#fff' : '#0f0f0f'}
                          fillRule="nonzero"
                          stroke="none"
                          strokeWidth="1"
                          strokeLinecap="butt"
                          strokeLinejoin="miter"
                          strokeMiterlimit="10"
                          strokeDasharray=""
                          strokeDashoffset="0"
                          fontFamily="none"
                          fontWeight="none"
                          fontSize="none"
                          textAnchor="none"
                        >
                          <g transform="scale(5.33333,5.33333)">
                            <path d="M12.5,4c-2.4675,0 -4.5,2.0325 -4.5,4.5v31c0,2.4675 2.0325,4.5 4.5,4.5h23c2.4675,0 4.5,-2.0325 4.5,-4.5v-21c-0.00008,-0.3978 -0.15815,-0.77928 -0.43945,-1.06055l-0.01562,-0.01562l-12.98437,-12.98437c-0.28127,-0.2813 -0.66275,-0.43938 -1.06055,-0.43945zM12.5,7h11.5v8.5c0,2.4675 2.0325,4.5 4.5,4.5h8.5v19.5c0,0.8465 -0.6535,1.5 -1.5,1.5h-23c-0.8465,0 -1.5,-0.6535 -1.5,-1.5v-31c0,-0.8465 0.6535,-1.5 1.5,-1.5zM27,9.12109l7.87891,7.87891h-6.37891c-0.8465,0 -1.5,-0.6535 -1.5,-1.5z" />
                          </g>
                        </g>
                      </svg>
                    </div>
                    {message.attachment.file}
                  </a>
                );
              default:
                return null;
            }
          })()}
          <span>{message.message}</span>
          <div className="chat-single-message-info">
            <span>{message.time.split(':').slice(0, 2).join(':')}</span>
            <span className="checkmark">
              {message.isRead ? (
                <>
                  <span className="checked">✓</span>
                  <span className="checked second">✓</span>
                </>
              ) : (
                <span className="checked">✓</span>
              )}
            </span>
          </div>
        </div>
        <ChatAttachmentImageModal
          src={message.attachment?.link}
          setActive={setModalOpen}
          active={isModalOpen}
        />
      </div>
    </>
  );
};

export default ChatSingleMessage;
