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
    ClosedMenuUser
} from '../../../../assets/Icons.jsx';

const MenuButton = ({title}) => {

    const iconMapping = {
        'Главная': <HomeIcon/>,
        'Shorts': <ShortsIcon/>,
        'Подписки': <SubscriptionsIcon/>,
        'Мой канал': <MyChannel/>,
        'История': <HistoryIcon/>,
        'Ваши видео': <LibraryIcon/>,
        'Смотреть позже': <WatchLater/>,
        'Развернуть': <ShowMore/>,
        'YouTube Premium': <PremiumIcon/>,
        'YouTube Music': <YoutubeMusic/>,
        'Настройки': <SettingsIcon/>,
        'Жалобы': <FlagIcon/>,
        'Справка': <HelpIcon/>,
        'Отправить отзыв': <AlertIcon/>,
        'В тренде': <Popular/>,
        'Музыка': <Music/>,
        'Фильмы': <Films/>,
        'Видеоигры': <Games/>,
        'Спорт': <Sport/>,
        'Творческая студия': <Studio/>,
        'Вы': <ClosedMenuUser/>
    };

    return (
        <>
            <button className='sidebar-button'>
                <div className='svg-container'>
                    {iconMapping[title]}
                </div>
                <p className='sidebar-text'>{title}</p>
            </button>
        </>
    );
}

export default MenuButton;
