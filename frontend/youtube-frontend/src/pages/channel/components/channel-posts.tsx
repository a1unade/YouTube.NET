import PostSingleItem from '../../../components/post';
import { PostItemType } from '../../../types/post/post-item-type.ts';
import { ChannelShortType } from '../../../types/channel/channel-short-type.ts';
import React from 'react';

const ChannelPosts = (props: {
  channel: ChannelShortType;
  setSaveVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
  setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { channel, setSaveVideoActive, setReportVideoActive } = props;

  const posts: PostItemType[] = [
    {
      id: '1',
      text: 'Первый пост. Сегодня я решил поделиться с вами красивым видом из окна моего офиса. Солнце светит, и вокруг зелень, которая радует глаз. Как вам такой вид?',
      attachment: null,
      likeCount: 34,
      dislikeCount: 1,
      commentCount: 10,
      date: '3 года назад',
    },
    {
      id: '2',
      text: 'Второй пост. Вчера я посмотрел новый фильм и остался в полном восторге! Сюжет захватывающий, а игра актеров на высшем уровне. Рекомендую всем!',
      attachment: null,
      likeCount: 54,
      dislikeCount: 3,
      commentCount: 5,
      date: '3 года назад',
    },
    {
      id: '3',
      text: 'Третий пост. Сегодня я приготовил вкусный обед. Это популярное блюдо, которое подарит вам много приятных эмоций. Отправляйтесь на кухню и попробуйте!',
      attachment: {
        type: 'image',
        file: 'https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg',
      },
      likeCount: 5,
      dislikeCount: 0,
      commentCount: 2,
      date: '3 года назад',
    },
    {
      id: '4',
      text: 'Четвертый пост. В выходные я посетил интересную выставку современного искусства. Это было захватывающее путешествие в мир креативных идей! Обязательно расскажу об этом подробнее позже.',
      attachment: {
        type: 'image',
        file: 'https://i.pinimg.com/564x/01/2b/0b/012b0bc2e871fc073c8dbf8008bdf20e.jpg',
      },
      likeCount: 20,
      dislikeCount: 2,
      commentCount: 8,
      date: '3 года назад',
    },
    {
      id: '5',
      text: 'Пятый пост. Проведите время на свежем воздухе! Выходные — идеальное время для прогулки в парке. Вдохновляйтесь природой и получайте удовольствие от жизни.',
      attachment: null,
      likeCount: 15,
      dislikeCount: 1,
      commentCount: 3,
      date: '3 года назад',
    },
  ];

  return (
    <>
      <div className="posts-list" style={{ marginBottom: 100 }}>
        {posts.map((post) => (
          <PostSingleItem
            key={post.id}
            post={post}
            channel={channel}
            setReportVideoActive={setReportVideoActive}
            setSaveActive={setSaveVideoActive}
          />
        ))}
      </div>
    </>
  );
};

export default ChannelPosts;
