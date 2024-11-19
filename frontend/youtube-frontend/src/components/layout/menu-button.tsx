import {
  AlertIcon,
  MyChannel,
  FlagIcon,
  HelpIcon,
  HistoryIcon,
  HomeIcon,
  LibraryIcon,
  YoutubeMusic,
  Popular,
  PremiumIcon,
  SettingsIcon,
  ShortsIcon,
  ShowMore,
  SubscriptionsIcon,
  WatchLater,
  Music,
  Films,
  Games,
  Sport,
  Studio,
  ClosedMenuUser,
  ButtonLikeIcon,
  PlaylistIcon,
  HomeIconFilled,
  ShortsIconFilled,
  SubscriptionsIconFilled,
  HistoryIconFilled,
  WatchLaterFilled,
  SettingsIconFilled,
  FlagIconFilled,
  PopularFilled,
  MusicFilled,
  FilmsFilled,
  GamesFilled,
  SportFilled,
  PlaylistIconFilled,
  ButtonLikeIconFilled,
  LibraryIconFilled,
} from '../../assets/icons.tsx';
import { IconMapping } from '../../types/icon/icons.ts';
import { MouseEventHandler } from 'react';

/**
 * Компонент кнопки для меню.
 *
 * Отображает кнопку с иконкой и текстом, которая изменяет свое состояние
 * в зависимости от того, выбрана ли она, и вызывает обработчик клика.
 *
 * @param {Object} props - Свойства компонента.
 * @param {string} props.title - Заголовок кнопки.
 * @param {string} props.selected - Текущий выбранный элемент меню.
 * @param {MouseEventHandler<HTMLButtonElement> | undefined} props.onClick - Функция для обработки клика по кнопке.
 *
 * @returns {JSX.Element} Возвращает элемент интерфейса кнопки меню с иконкой и текстом.
 *
 * @example Пример использования:
 *   <MenuButton title="Главная" selected={selected} onClick={handleClick} />
 */

const MenuButton = (props: {
  title: string;
  selected: string;
  onClick: MouseEventHandler<HTMLButtonElement> | undefined;
}): JSX.Element => {
  const { title, selected, onClick } = props;
  // prettier-ignore
  const iconMapping: IconMapping = {
		"Главная": { default: <HomeIcon />, filled: <HomeIconFilled /> },
		"Shorts": { default: <ShortsIcon />, filled: <ShortsIconFilled /> },
		"Подписки": {
			default: <SubscriptionsIcon />,
			filled: <SubscriptionsIconFilled />,
		},
		"Мой канал": { default: <MyChannel />, filled: <MyChannel /> },
		"История": { default: <HistoryIcon />, filled: <HistoryIconFilled /> },
		"Ваши видео": {
			default: <LibraryIcon />,
			filled: <LibraryIconFilled />,
		},
		"Смотреть позже": {
			default: <WatchLater />,
			filled: <WatchLaterFilled />,
		},
		"Развернуть": { default: <ShowMore />, filled: <ShowMore /> },
		"YouTube Premium": {
			default: <PremiumIcon />,
			filled: <PremiumIcon />,
		},
		"YouTube Music": {
			default: <YoutubeMusic />,
			filled: <YoutubeMusic />,
		},
		"Настройки": {
			default: <SettingsIcon />,
			filled: <SettingsIconFilled />,
		},
		"Жалобы": { default: <FlagIcon />, filled: <FlagIconFilled /> },
		"Поддержка": { default: <HelpIcon />, filled: <HelpIcon /> },
		"Отправить отзыв": { default: <AlertIcon />, filled: <AlertIcon /> },
		"В тренде": { default: <Popular />, filled: <PopularFilled /> },
		"Музыка": { default: <Music />, filled: <MusicFilled /> },
		"Фильмы": { default: <Films />, filled: <FilmsFilled /> },
		"Видеоигры": { default: <Games />, filled: <GamesFilled /> },
		"Спорт": { default: <Sport />, filled: <SportFilled /> },
		"Творческая студия": { default: <Studio />, filled: <Studio /> },
		"Вы": { default: <ClosedMenuUser />, filled: <ClosedMenuUser /> },
		"Понравившиеся": {
			default: <ButtonLikeIcon />,
			filled: <ButtonLikeIconFilled />,
		},
		"Плейлисты": {
			default: <PlaylistIcon />,
			filled: <PlaylistIconFilled />,
		},
	};

  return (
    <>
      <button
        id={`button-${title}`}
        onClick={onClick}
        className={`sidebar-button ${selected === title ? 'menu-button-selected' : ''}`}
      >
        <div className="svg-container">
          {selected == title ? iconMapping[title].filled : iconMapping[title].default}
        </div>
        <p className="sidebar-text">{title}</p>
      </button>
    </>
  );
};

export default MenuButton;
