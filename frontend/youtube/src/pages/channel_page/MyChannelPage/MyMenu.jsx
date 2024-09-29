import {  useLocation } from 'react-router-dom';
import {useEffect, useState} from "react";

// eslint-disable-next-line react/prop-types
const MyMenu = ({ channelName }) => {
    const [activeButton, setActiveButton] = useState(1); // Состояние для активной кнопки
    const location = useLocation();

    useEffect(() => {
        switch (location.pathname) {
            case `/MyChannel/${channelName}/featured`:
                setActiveButton(1);
                break;
            case `/MyChannel/${channelName}/videos`:
                setActiveButton(2);
                break;
            case `/MyChannel/${channelName}/playlists`:
                setActiveButton(3);
                break;
            case `/MyChannel/${channelName}/community`:
                setActiveButton(4);
                break;
            default:
                setActiveButton(1); // Если URL не соответствует ни одной из вкладок, устанавливаем первую кнопку по умолчанию
        }
    }, [location.pathname, channelName]);

    return (
        <>
            <div className="channel-menu">
                <a href={`/MyChannel/${channelName}/featured`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 1 ? 'active' : ''}`}
                        onClick={() => setActiveButton(1)}>
                        Главная
                    </button>
                </a>
                <a href={`/MyChannel/${channelName}/videos`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 2 ? 'active' : ''}`}
                        onClick={() => setActiveButton(2)}>
                        Видео
                    </button>
                </a>
                <a href={`/MyChannel/${channelName}/playlists`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 3 ? 'active' : ''}`}
                        onClick={() => setActiveButton(3)}>
                        Плейлисты
                    </button>
                </a>
                <a href={`/MyChannel/${channelName}/community`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 4 ? 'active' : ''}`}
                        onClick={() => setActiveButton(4)}>

                        Сообщество
                    </button>
                </a>
            </div>
            <hr className="separator"/>
        </>
    );
};

export default MyMenu;