import React from "react";
import { validateEmail } from "../../../utils/validator";
import { useNavigate } from 'react-router-dom';

const Email = ({ setEmail, email, setContainerContent, containerContent }) => {
    const navigate = useNavigate()

    const handleNextButtonClick = () => {
        const message = validateEmail(email, true);
        if (message.length > 0) {
            document.getElementById("email").classList.add("error", "shake");
            document.getElementById("error").classList.remove("hidden");
            document.getElementById("message").textContent = message;
            setTimeout(() => {
                document.getElementById("email").classList.remove("shake");
            }, 500);
        } else {
            setContainerContent(containerContent + 1);
        }
    }

    return (
        <>
            <h1>Вход</h1>
            <span>Перейдите на YouTube</span>
            <div className="input-container">
                <input type="email" value={email} id="email" placeholder="Телефон или адрес эл. почты" onChange={(e) => setEmail(e.target.value)}></input>
                <label>Телефон или адрес эл. почты</label>
                <div id="error" className="error-message hidden">
                    <span>
                        <svg aria-hidden="true" fill="currentColor" focusable="false" width="16px" height="16px" viewBox="0 0 24 24" xmlns="https://www.w3.org/2000/svg">
                            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"></path>
                        </svg>
                    </span>
                    <span id="message"></span>
                </div>
                <a href=""><b>Забыли адрес электронной почты?</b></a>
            </div>
            <div className="input-container">
                <span>Работаете на чужом компьютере? Включите гостевой режим.</span>
                <a href=""><b>Подробнее об использовании гостевого режима</b></a>
            </div>
            <div className="sign-buttons">
                <button className="left-button" onClick={() => navigate("/signup")}>Создать аккаунт</button>
                <button className="right-button" onClick={handleNextButtonClick}>Далее</button>
            </div>
        </>
    );
}

export default Email;