import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { navigationMap } from '../../../types/channel/channel-navigation-map.ts';

const ChannelNavigation = (props: {
  selected: string;
  setSelected: React.Dispatch<React.SetStateAction<string>>;
}) => {
  const { selected, setSelected } = props;
  const { id } = useParams();
  const navigate = useNavigate();

  const handleSelect = (title: string) => {
    const currentButton = document.getElementById(`channel-button-${selected}`)!;
    currentButton.classList.remove('selected');

    setSelected(title);

    const newKey = Object.keys(navigationMap).find((key) => navigationMap[key] === title);
    if (newKey) {
      navigate(`/channel/${id}/${newKey}`);
    }
  };

  return (
    <div className="channel-navigation-button-container">
      <button
        id="channel-button-Видео"
        className={selected === 'Видео' ? 'selected' : ''}
        onClick={() => handleSelect('Видео')}
      >
        Видео
      </button>
      <button
        id="channel-button-Плейлисты"
        className={selected === 'Плейлисты' ? 'selected' : ''}
        onClick={() => handleSelect('Плейлисты')}
      >
        Плейлисты
      </button>
      <button
        id="channel-button-Сообщество"
        className={selected === 'Сообщество' ? 'selected' : ''}
        onClick={() => handleSelect('Сообщество')}
      >
        Сообщество
      </button>
      <button
        id="channel-button-О канале"
        className={selected === 'О канале' ? 'selected' : ''}
        onClick={() => handleSelect('О канале')}
      >
        О канале
      </button>
    </div>
  );
};

export default ChannelNavigation;
