/* istanbul ignore file */
import { useEffect, useState } from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';

// prettier-ignore
const navigationMap: { [p: string]: string } = {
  'featured': 'Главная',
  'videos': 'Видео',
  'playlists': 'Плейлисты',
  'community': 'Сообщество'
};

const ChannelNavigation = () => {
  const { id } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const [selected, setSelected] = useState<string>('Главная');

  useEffect(() => {
    const pathSegments = location.pathname.split('/');
    const lastSegment = pathSegments[pathSegments.length - 1];

    if (navigationMap[lastSegment]) {
      setSelected(navigationMap[lastSegment]);
    } else {
      setSelected('Главная');
    }
  }, [location.pathname]);

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
        id="channel-button-Главная"
        className={selected === 'Главная' ? 'selected' : ''}
        onClick={() => handleSelect('Главная')}
      >
        Главная
      </button>
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
    </div>
  );
};

export default ChannelNavigation;
