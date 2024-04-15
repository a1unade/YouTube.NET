import {  useLocation } from 'react-router-dom';
import {useEffect, useState} from "react";

const MenuForFilms = () => {
    const [activeButton, setActiveButton] = useState(1); // Состояние для активной кнопки
    const location = useLocation();

    useEffect(() => {
        switch (location.pathname) {
            case `/feed/Catalog`:
                setActiveButton(1);
                break;
            case `/feed/Purchases`:
                setActiveButton(2);
                break;

            default:
                setActiveButton(1); // Если URL не соответствует ни одной из вкладок, устанавливаем первую кнопку по умолчанию
        }
    }, [location.pathname]);

    return (
        <>
            <div className="film-menu">
                <a href={`/feed/Catalog`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 1 ? 'active' : ''}`}
                        onClick={() => setActiveButton(1)}>
                        Каталог
                    </button>
                </a>
                <a href={`/feed/Purchases`}>
                    <button
                        className={`channel-menu-btn ${activeButton === 2 ? 'active' : ''}`}
                        onClick={() => setActiveButton(2)}>
                        Покупки
                    </button>
                </a>

            </div>
            <hr className="separator" />
        </>
    );
};

export default MenuForFilms;
