import { ChatMessage } from "../../../interfaces/chat/chat-message.ts";
import { useState } from "react";
import ChatAttachmentImageModal from "../../../components/modal/chat-image-attachment-modal.tsx";

const ChatSingleMessage = (props: {
  message: ChatMessage;
  userId: string | null;
}) => {
  const { message, userId } = props;
  const [isModalOpen, setModalOpen] = useState(false);

  return (
    <div className="chat-message-layout">
      <div
        className={
          message.senderId === userId
            ? "chat-single-message admin-message"
            : "chat-single-message user-message"
        }
      >
        {(() => {
          const contentType = message.attachment?.contentType || "";

          if (contentType.includes("image")) {
            return (
              <img
                style={{ cursor: "pointer" }}
                src={`http://localhost:8080/File/GetFileStream/${message.attachment?.fileId}`}
                alt="attachment"
                onClick={() => setModalOpen(true)}
              />
            );
          } else if (contentType.includes("application")) {
            return (
              <a
                href={`http://localhost:8080/File/GetFileStream/${message.attachment?.fileId}`}
                download
              >
                <div style={{ width: "fit-content", height: "fit-content" }}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    x="0px"
                    y="0px"
                    width="27"
                    height="27"
                    viewBox="0,0,300,150"
                  >
                    <g
                      fill={message.senderId !== userId ? "#fff" : "#0f0f0f"}
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
                {message.attachment?.fileId}
              </a>
            );
          }

          return null;
        })()}
        <span>{message.message}</span>
        <div className="chat-single-message-info">
          <span>{message.time.split(":").slice(0, 2).join(":")}</span>
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
        src={`http://localhost:8080/File/GetFileStream/${message.attachment?.fileId}`}
        setActive={setModalOpen}
        active={isModalOpen}
      />
    </div>
  );
};

export default ChatSingleMessage;
