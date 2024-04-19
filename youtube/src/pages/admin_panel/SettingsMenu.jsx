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

    function RedirectToChannel() {
        window.location.href = "https://youtube.com/"; // Замените на ваш URL канала
    }

    function ClearInputs() {
        const inputs = document.querySelectorAll('input[type="text"], input[type="email"], textarea');
        inputs.forEach(input => {
            input.value = '';
        });
    }

    function handleSubmit() {
        // Добавьте код для отправки данных, например, с помощью AJAX запроса
        // Например:
        // const formData = new FormData(document.getElementById('yourFormId'));
        // fetch('/submit', {
        //     method: 'POST',
        //     body: formData
        // })
        // .then(response => {
        //     // Обработка успешного ответа
        // })
        // .catch(error => {
        //     // Обработка ошибки
        // });
    }

    return (
        <>
            <div className="admin-menu">
                <div className="menu-panel">
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

                <div className="channel-settings-buttons">
                    <div className="submit-buttons">
                        <button onClick={() => RedirectToChannel()}>
                            ПЕРЕЙТИ НА КАНАЛ
                        </button>
                        <button onClick={() => ClearInputs()}>
                            ОТМЕНА
                        </button>
                        <button id="submit-button" onClick={() => handleSubmit()}>
                            ОПУБЛИКОВАТЬ
                        </button>
                    </div>
                </div>
            </div>

            <hr className="separator"/>
        </>
    );
};
export default SettingsMenu;