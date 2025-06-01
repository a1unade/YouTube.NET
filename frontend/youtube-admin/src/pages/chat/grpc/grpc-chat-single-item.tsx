import { ChatSingleItem } from "../../../interfaces/chat/chat-single-item.ts";
import React from "react";
import { useNavigate } from "react-router-dom";

const GrpcChatSingleItem = (props: {
  chat: ChatSingleItem;
  selected: string | null;
  setSelected: React.Dispatch<React.SetStateAction<string | null>>;
}) => {
  const { chat, selected, setSelected } = props;
  const navigate = useNavigate();

  return (
    <div
      className={
        selected === chat.chatId
          ? "chat-single-item-layout selected-chat"
          : "chat-single-item-layout"
      }
      onClick={() => {
        setSelected(chat.chatId);
        navigate(`/grpc-chat/${chat.chatId}`);
      }}
    >
      <img
        src={
          chat.avatarUrl ||
          "https://avatars.githubusercontent.com/u/113981832?v=4"
        }
        className="circular-avatar"
        alt=""
      />
      <div className="chat-single-item-body">
        <div className="chat-single-item-detailed">
          <span style={{ fontSize: 14, fontWeight: 800 }}>{chat.userName}</span>
          <span>{chat.lastMessage?.message}</span>
        </div>
        <div className="chat-single-item-special">
          <span style={{ fontSize: 10 }}>
            {chat.lastMessage?.time.split(":").slice(0, 2).join(":")}
          </span>
          {!chat.lastMessage || !chat.lastMessage.isRead ? (
            <div className="chat-single-item-notification" />
          ) : null}
        </div>
      </div>
    </div>
  );
};

export default GrpcChatSingleItem;
