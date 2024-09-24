import {useEffect, useState} from "react";
import {useLocation} from "react-router-dom";

// const AdminMenu = ({ channelId }) => {
const AdminMenu = () => {
    const [activeButton, setActiveButton] = useState(1); // Состояние для активной кнопки
    const location = useLocation();

    useEffect(() => {
        switch (location.pathname) {
            case `/channel/edit/addvideo`:
                setActiveButton(1);
                break;
            case `/channel/edit/channelId/playlist`:
                setActiveButton(2);
                break;
            default:
                setActiveButton(1); // Если URL не соответствует ни одной из вкладок, устанавливаем первую кнопку по умолчанию
        }
    }, [location.pathname/*, channelId*/]);

    return (
        <>
            <div className="admin-menu">
                <a href={`/channel/edit/addvideo`}>
                    <button
                        className={`admin-menu-btn ${activeButton === 1 ? 'admin-active' : ''}`}
                        onClick={() => setActiveButton(1)}>
                        Видео
                    </button>
                </a>
                <a href={`/channel/edit/channelId/playlist`}>
                    <button
                        className={`admin-menu-btn ${activeButton === 2 ? 'admin-active' : ''}`}
                        onClick={() => setActiveButton(2)}>
                        Плейлисты
                    </button>
                </a>
            </div>
            <hr className="separator"/>
        </>
    );
}
export default AdminMenu;