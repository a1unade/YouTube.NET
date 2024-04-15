import {useEffect, useState} from "react";
import {useLocation} from "react-router-dom";

const TrendMenu = () => {

    const [activeButton, setActiveButton] = useState(1); // Состояние для активной кнопки
    const location = useLocation();

    useEffect(() => {
        switch (location.pathname) {
            case `/feed/trending`:
                setActiveButton(1);
                break;
            case `/feed/trending/music`:
                setActiveButton(2);
                break;
            case `/feed/trending/videogames`:
                setActiveButton(3);
                break;
            case `/feed/trending/films`:
                setActiveButton(4);
                break;

            default:
                setActiveButton(1); // Если URL не соответствует ни одной из вкладок, устанавливаем первую кнопку по умолчанию
        }
    }, [location.pathname]);
    return(
        <>

            <div className="film-menu">
                <a href={`/feed/trending`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 1 ? 'active' : ''}`}
                        onClick={() => setActiveButton(1)}>
                        Новости
                    </button>
                </a>
                <a href={`/feed/trending/music`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 2 ? 'active' : ''}`}
                        onClick={() => setActiveButton(2)}>
                        Музыка
                    </button>
                </a>
                <a href={`/feed/trending/videogames`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 3 ? 'active' : ''}`}
                        onClick={() => setActiveButton(3)}>
                        Видеоигры
                    </button>
                </a>
                <a href={`/feed/trending/films`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 4 ? 'active' : ''}`}
                        onClick={() => setActiveButton(4)}>
                        Фильмы
                    </button>
                </a>

            </div>
            <hr className="separator"/>
        </>
    );
};
export default TrendMenu;