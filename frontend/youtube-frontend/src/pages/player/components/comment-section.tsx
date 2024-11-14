import { useState } from 'react';
import Index from '../../../components/comment';
import { CommentType } from '../../../types/comment/comment-type.ts';

const CommentSection = () => {
  const [isFocused, setIsFocused] = useState(false);

  const comments: CommentType[] = [
    {
      id: 1,
      authorDisplayName: 'Иван Иванов',
      authorProfileImageUrl: 'https://randomuser.me/api/portraits/men/1.jpg',
      textDisplay: 'Отличная статья! Очень полезная информация.',
    },
    {
      id: 2,
      authorDisplayName: 'Мария Петрова',
      authorProfileImageUrl: 'https://randomuser.me/api/portraits/women/1.jpg',
      textDisplay: 'Согласна с предыдущим комментатором. Спасибо за материалы!',
    },
    {
      id: 3,
      authorDisplayName: 'Алексей Сидоров',
      authorProfileImageUrl: 'https://randomuser.me/api/portraits/men/2.jpg',
      textDisplay: 'Не совсем согласен. Есть другие мнения на этот счет.',
    },
    {
      id: 4,
      authorDisplayName: 'Елена Кузнецова',
      authorProfileImageUrl: 'https://randomuser.me/api/portraits/women/2.jpg',
      textDisplay: 'Очень интересно, жду продолжения!',
    },
    {
      id: 5,
      authorDisplayName: 'Дмитрий Смирнов',
      authorProfileImageUrl: 'https://randomuser.me/api/portraits/men/3.jpg',
      textDisplay: 'Спасибо за ваш труд! Полезные советы.',
    },
  ];

  const handleFocus = () => {
    setIsFocused(true);
  };

  const handleCancel = () => {
    setIsFocused(false);
  };

  return (
    <div className="player-comment-section">
      <h2>Комментарии: 675</h2>
      <div className="player-leave-comment-section">
        <img
          style={{ width: 40, height: 40 }}
          className="circular-avatar"
          src="https://sm.ign.com/ign_nordic/cover/a/avatar-gen/avatar-generations_prsz.jpg"
          alt=""
        />
        <div className="player-input-section">
          <textarea
            id="comment-textarea"
            style={{ resize: 'none' }}
            onFocus={handleFocus}
            onBlur={() => setIsFocused(false)}
            placeholder="Введите комментарий"
          />
          {isFocused && (
            <div className="player-comment-buttons">
              <button id="True" className="subscribe-button" onClick={handleCancel}>
                Отмена
              </button>
              <button id="True" className="subscribe-button">
                Оставить комментарий
              </button>
            </div>
          )}
        </div>
      </div>
      <div className="player-comments-list">
        {comments.map((comment) => (
          <Index key={comment.id} comment={comment} />
        ))}
      </div>
    </div>
  );
};

export default CommentSection;
