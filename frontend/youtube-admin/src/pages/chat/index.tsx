import ChatSingleItem from "./components/chat-single-item.tsx";
import ChatWindow from "./components/chat-window.tsx";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

const ChatPage = () => {
  const { id } = useParams<{ id: string }>(); // Убедитесь, что тип id указан
  const [selected, setSelected] = useState<string | null>(id || null);
  const navigate = useNavigate();

  const handleKeyDown = (event: KeyboardEvent) => {
    if (event.key === "Escape") {
      setSelected(null);
      navigate("/chat");
    }
  };

  useEffect(() => {
    window.addEventListener("keydown", handleKeyDown);

    return () => {
      window.removeEventListener("keydown", handleKeyDown);
    };
  }, []);

  const chatList: ChatSingleItem[] = [
    {
      id: "1",
      user_id: "user_01",
      last_message: "Привет, как дела?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "01.10",
    },
    {
      id: "2",
      user_id: "user_02",
      last_message: "Мне нужна помощь с заданием!",
      needs_help: true,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "02.10",
    },
    {
      id: "3",
      user_id: "user_03",
      last_message: "Когда будет следующее занятие?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "03.10",
    },
    {
      id: "4",
      user_id: "user_04",
      last_message: "Я так рад тебя видеть!",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "04.10",
    },
    {
      id: "5",
      user_id: "user_05",
      last_message: "Помоги мне, пожалуйста!",
      needs_help: true,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "05.10",
    },
    {
      id: "6",
      user_id: "user_06",
      last_message: "Что ты думаешь о проекте?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "06.10",
    },
    {
      id: "7",
      user_id: "user_07",
      last_message: "Есть какие-то новости?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "07.10",
    },
    {
      id: "8",
      user_id: "user_08",
      last_message: "Не знаю, как решить эту задачу.",
      needs_help: true,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "08.10",
    },
    {
      id: "9",
      user_id: "user_09",
      last_message: "Увидимся позже?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "09.10",
    },
    {
      id: "10",
      user_id: "user_10",
      last_message: "Когда ждать результаты?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "10.10",
    },
    {
      id: "11",
      user_id: "user_11",
      last_message: "Ты где?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "11.10",
    },
    {
      id: "12",
      user_id: "user_12",
      last_message: "Я не знаю, как это сделать.",
      needs_help: true,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "12.10",
    },
    {
      id: "13",
      user_id: "user_13",
      last_message: "Спасибо за твою помощь!",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "13.10",
    },
    {
      id: "14",
      user_id: "user_14",
      last_message: "Как у тебя дела?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "14.10",
    },
    {
      id: "15",
      user_id: "user_15",
      last_message: "Мне нужна помощь с проектом.",
      needs_help: true,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "15.10",
    },
    {
      id: "16",
      user_id: "user_16",
      last_message: "Это было замечательно!",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "16.10",
    },
    {
      id: "17",
      user_id: "user_17",
      last_message: "Мне нужно больше информации.",
      needs_help: true,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "17.10",
    },
    {
      id: "18",
      user_id: "user_18",
      last_message: "Где мы встречаемся?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "18.10",
    },
    {
      id: "19",
      user_id: "user_19",
      last_message: "Какой у нас план на сегодня?",
      needs_help: false,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "19.10",
    },
    {
      id: "20",
      user_id: "user_20",
      last_message: "Я не поняла задание.",
      needs_help: true,
      avatar:
        "https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg",
      date: "20.10",
    },
  ];

  return (
    <div className="chat-page-layout">
      <div className="chat-list-section">
        <div>
          <div className="chat-list-section-header">
            <span>Чаты</span>
          </div>
          {chatList.map((chat: ChatSingleItem) => (
            <ChatSingleItem
              selected={selected}
              setSelected={setSelected}
              key={chat.id}
              chat={chat}
            />
          ))}
        </div>
      </div>
      <ChatWindow
        selected={selected}
        chat={chatList.find((chat) => chat.id === selected)}
      />
    </div>
  );
};

export default ChatPage;
