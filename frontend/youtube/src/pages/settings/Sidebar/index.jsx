import './style.css';
import {useNavigate} from "react-router-dom";

const Sidebar = () => {
    const navigate = useNavigate();

    return (
        <div className="sidebar">
            <div className="best-youtube-section-title-container">
                <p className="youtube-title-style">Настройки</p>
            </div>
            <button className="sidebar-button" onClick={() => navigate("/settings/account")}>
                <p className="sidebar-text">Аккаунт</p>
            </button>
            <button className="sidebar-button" onClick={() => navigate("/settings/payments")}>
                <p className="sidebar-text">Расчеты и платежи</p>
            </button>
        </div>
    );
};

export default Sidebar;