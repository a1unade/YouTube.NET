import { ChatSingleItem } from "../../../interfaces/chat/chat-single-item.ts";
import React from "react";
import { useNavigate } from "react-router-dom";

const ChatSingleItem = (props: {
  chat: ChatSingleItem;
  selected: string | null;
  setSelected: React.Dispatch<React.SetStateAction<string | null>>;
}) => {
  const { chat, selected, setSelected } = props;
  const navigate = useNavigate();

  return (
    <div
      className={
        selected === chat.id
          ? "chat-single-item-layout selected-chat"
          : "chat-single-item-layout"
      }
      onClick={() => {
        chat.needs_help = false;
        setSelected(chat.id);
        navigate(`/chat/${chat.id}`);
      }}
    >
      <img src={chat.avatar} className="circular-avatar" alt="" />
      <div className="chat-single-item-body">
        <div className="chat-single-item-detailed">
          <span style={{ fontSize: 14, fontWeight: 800 }}>{chat.user_id}</span>
          <span>{chat.last_message}</span>
        </div>
        <div className="chat-single-item-special">
          <span style={{ fontSize: 10 }}>{chat.date}</span>
          {chat.needs_help && <div className="chat-single-item-notification" />}
        </div>
      </div>
    </div>
  );
};

export default ChatSingleItem;
