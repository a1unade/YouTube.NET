import { useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ChatMessage } from "../../../interfaces/chat/chat-message.ts";
import apiClient from "../../../utils/apiClient.ts";
import { ChatCollectionResponse } from "../../../interfaces/chat/chat-collection-response.ts";
import { useGrpc } from "../../../hooks/chat/use-grpc.ts";
import GrpcChatWindow from "./grpc-chat-window.tsx";
import GrpcChatSingleItem from "./grpc-chat-single-item.tsx";
import { ChatSingleItem } from "../../../interfaces/chat/chat-single-item.ts";

const GrpcChatPage = (props: { userId: string | null }) => {
  const { userId } = props;
  const { id } = useParams<{ id: string }>();
  const [pageCount, setPageCount] = useState(0);
  const [fetching, setFetching] = useState(true);
  const [page, setPage] = useState(1);
  const [chatId, setChatId] = useState<string | null>(id || null);
  const [chatMessages, setChatMessages] = useState<ChatMessage[]>([]);
  const [chatList, setChatList] = useState<ChatSingleItem[]>([]);
  const componentRef = useRef<HTMLDivElement | null>(null);

  const navigate = useNavigate();
  const { joinChat, sendMessage, leaveChat, isConnected } = useGrpc({
    setChatId,
    setChatMessages,
  });

  useEffect(() => {
    const currentRef = componentRef.current;
    if (currentRef) {
      currentRef.addEventListener("scroll", handleScroll);
    }

    return () => {
      if (currentRef) {
        currentRef.removeEventListener("scroll", handleScroll);
      }
    };
  }, [chatList, pageCount, setFetching]);

  useEffect(() => {
    if (fetching) {
      apiClient
        .get<ChatCollectionResponse>(
          `Chat/GetHistoryCollectionForCard?Page=${page}&Size=10`,
        )
        .then((response) => {
          if (response.data.chatCardDtos !== null) {
            setChatList((prevData) => [
              ...prevData,
              ...response.data.chatCardDtos,
            ]);
          }

          if (pageCount === 0) {
            setPageCount(response.data.pageCount);
          }
        })
        .catch((error) => {
          console.error("Error fetching data:", error);
        })
        .finally(() => setFetching(false));
    }
  }, [fetching]);

  const handleKeyDown = (event: KeyboardEvent) => {
    if (event.key === "Escape") {
      setChatId(null);
      navigate("/chat");
      leaveChat();
    }
  };

  const handleScroll = (event: Event) => {
    const target = event.target as Document;
    if (
      target.documentElement.scrollHeight -
        (target.documentElement.scrollTop + window.innerHeight) <
        600 &&
      page < pageCount
    ) {
      setFetching(true);
      setPage(page + 1);
    }
  };

  useEffect(() => {
    window.addEventListener("keydown", handleKeyDown);

    return () => {
      window.removeEventListener("keydown", handleKeyDown);
    };
  }, []);

  return (
    <div className="chat-page-layout">
      <div className="chat-list-section" ref={componentRef}>
        <div>
          <div className="chat-list-section-header">
            <span>Чаты</span>
          </div>
          {chatList !== null
            ? chatList.map((chat: ChatSingleItem) => (
                <GrpcChatSingleItem
                  selected={chatId}
                  setSelected={setChatId}
                  key={chat.chatId}
                  chat={chat}
                />
              ))
            : null}
        </div>
      </div>
      <GrpcChatWindow
        chatMessages={chatMessages}
        setChatMessages={setChatMessages}
        chatId={chatId}
        userId={userId}
        joinChat={joinChat}
        sendMessage={sendMessage}
        isConnected={isConnected}
        chat={
          chatList !== null
            ? chatList.find((chat) => chat.chatId === chatId)
            : undefined
        }
      />
    </div>
  );
};

export default GrpcChatPage;
