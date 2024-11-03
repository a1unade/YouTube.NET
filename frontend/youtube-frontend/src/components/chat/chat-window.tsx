import ChatSingleMessage from './chat-single-message.tsx';
import ChatWindowInputSection from './chat-window-input-section.tsx';
import { ChatMessage } from '../../interfaces/chat/chat-message.ts';

const ChatWindow = () => {
  const chatMessages: ChatMessage[] = [
    {
      message: 'Привет! Как дела?',
      author: 'user_01',
      attachment: null,
      time: '12:22',
      is_read: false,
    },
    {
      message: 'Мне нужна помощь с заданием.',
      author: 'admin',
      attachment: {
        type: 'image',
        file: 'photo1.jpg',
        link: 'https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg',
      },
      time: '12:21',
      is_read: true,
    },
    {
      message: 'Какой у тебя любимый цвет?',
      author: 'user_01',
      attachment: null,
      time: '12:20',
      is_read: true,
    },
    {
      message: 'Вот документ, который может быть полезен.',
      author: 'admin',
      attachment: {
        type: 'file',
        file: 'document.pdf',
        link: 'https://example.com/document.pdf',
      },
      time: '12:19',
      is_read: true,
    },
    {
      message: 'Согласен с твоим мнением!',
      author: 'user_01',
      attachment: null,
      time: '12:18',
      is_read: true,
    },
    {
      message: 'Скинь мне, пожалуйста, ссылку на карту.',
      author: 'admin',
      attachment: null,
      time: '12:17',
      is_read: true,
    },
    {
      message: 'Как дела у твоего кота?',
      author: 'user_01',
      attachment: null,
      time: '12:16',
      is_read: true,
    },
    {
      message: 'Посмотри на это фото!',
      author: 'admin',
      attachment: {
        type: 'image',
        file: 'photo2.jpg',
        link: 'https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg',
      },
      time: '12:15',
      is_read: true,
    },
    {
      message: 'Что нового в школе?',
      author: 'user_01',
      attachment: null,
      time: '12:14',
      is_read: true,
    },
    {
      message: 'Это было забавно!',
      author: 'admin',
      attachment: null,
      time: '12:13',
      is_read: true,
    },
    {
      message: 'Не забудь про встречу завтра.',
      author: 'user_01',
      attachment: null,
      time: '12:12',
      is_read: true,
    },
    {
      message: 'Я прикрепил заметки.',
      author: 'admin',
      attachment: {
        type: 'file',
        file: 'notes.txt',
        link: 'https://example.com/notes.txt',
      },
      time: '12:11',
      is_read: true,
    },
    {
      message: 'Как прошел твой день?',
      author: 'user_01',
      attachment: null,
      time: '12:10',
      is_read: true,
    },
    {
      message: 'Вот ссылка на полезную статью.',
      author: 'admin',
      attachment: null,
      time: '12:09',
      is_read: true,
    },
    {
      message: 'Можешь отправить мне те заметки?',
      author: 'user_01',
      attachment: {
        type: 'file',
        file: 'document.docx',
        link: 'https://example.com/document.docx',
      },
      time: '12:08',
      is_read: true,
    },
    {
      message: 'Я всегда знал, что ты справишься!',
      author: 'admin',
      attachment: null,
      time: '12:07',
      is_read: true,
    },
    {
      message: 'Пока! Давай увидимся позже.',
      author: 'user_01',
      attachment: null,
      time: '12:06',
      is_read: true,
    },
    {
      message: 'Спасибо за помощь!',
      author: 'admin',
      attachment: null,
      time: '12:05',
      is_read: true,
    },
    {
      message: 'Увидимся на вечеринке в выходные.',
      author: 'user_01',
      attachment: null,
      time: '12:04',
      is_read: true,
    },
    {
      message: 'Успехов на экзамене!',
      author: 'admin',
      attachment: null,
      time: '12:03',
      is_read: true,
    },
    {
      message: 'Посмотри, что я нашел!',
      author: 'user_01',
      attachment: {
        type: 'image',
        file: 'photo3.jpg',
        link: 'https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg',
      },
      time: '12:02',
      is_read: true,
    },
    {
      message: 'Мы должны это повторить!',
      author: 'admin',
      attachment: null,
      time: '12:01',
      is_read: true,
    },
  ];

  return (
    <div className="chat-selected-layout">
      <div className="chat-section-layout">
        {chatMessages.map((message: ChatMessage, index: number) => (
          <ChatSingleMessage key={index} message={message} />
        ))}
      </div>
      <ChatWindowInputSection />
    </div>
  );
};

export default ChatWindow;
