import {useEffect, useState} from "react";
import {useLocation} from "react-router-dom";

// const SettingsMenu = (channelId) => {
const SettingsMenu = () => {
    const [activeButton, setActiveButton] = useState(1); // Состояние для активной кнопки
    const location = useLocation();

    useEffect(() => {
        switch (location.pathname) {
            case `/channel/edit/images`:
                setActiveButton(1);
                break;
            case `/channel/edit/details`:
                setActiveButton(2);
                break;
            default:
                setActiveButton(1); // Если URL не соответствует ни одной из вкладок, устанавливаем первую кнопку по умолчанию
        }
    }, [location.pathname/*, channelId*/]);

    return (
        <>
            <div className="admin-menu">
                <a href={`/channel/edit/images`}>
                    <button
                        className={`admin-menu-btn ${activeButton === 1 ? 'admin-active' : ''}`}
                        onClick={() => setActiveButton(1)}>
                        Брендинг
                    </button>
                </a>
                <a href={`/channel/edit/details`}>
                    <button
                        className={`admin-menu-btn ${activeButton === 2 ? 'admin-active' : ''}`}
                        onClick={() => setActiveButton(2)}>
                        Основные сведения
                    </button>
                </a>
            </div>
            <hr className="separator"/>
        </>
    );
};
export default SettingsMenu;