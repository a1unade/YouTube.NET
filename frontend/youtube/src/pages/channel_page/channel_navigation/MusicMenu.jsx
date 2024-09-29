import {useEffect, useState} from "react";
import {useLocation} from "react-router-dom";

const MusicMenu = () => {
    const [activeButton, setActiveButton] = useState(1); // Состояние для активной кнопки
    const location = useLocation();

    useEffect(() => {
        switch (location.pathname) {
            case `/channel/Music/featured`:
                setActiveButton(1);
                break;
            case `/channel/Music/community`:
                setActiveButton(2);
                break;

            default:
                setActiveButton(1);
        }
    }, [location.pathname]);

    return (
        <>
            <div className="film-menu" style={{marginLeft: 30}}>
                <a href={`/channel/Music/featured`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 1 ? 'active' : ''}`}
                        onClick={() => setActiveButton(1)}>
                        Главная
                    </button>
                </a>
                <a href={`/channel/Music/community`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 2 ? 'active' : ''}`}
                        onClick={() => setActiveButton(2)}>
                        Сообщество
                    </button>
                </a>

            </div>
            <hr className="separator" />
        </>
    );
}
export default MusicMenu;