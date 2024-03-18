import React from "react";
import { validateEmail } from "../../utils/validator";

const Email = ({ email, setEmail, setContainerContent, containerContent }) => {
    const handleNextButtonClick = () => {
        const message = validateEmail(email, false);
        if (message.length > 0) {
            document.getElementById("email").classList.add("error", "shake");
            document.getElementById("error").classList.remove("hidden");
            document.getElementById("message").textContent = message;
            setTimeout(() => {
                document.getElementById("email").classList.remove("shake");
            }, 500);
        }
        else {
            setContainerContent(containerContent + 1);
        }
    }

    return (
        <>
            <h1 style={{ marginBottom: 10 }}>Использовать существующий адрес электронной почты</h1>
            <span style={{ fontSize: 17 }}>Введите адрес электронной почты, который вы хотите использовать для своего аккаунта Google.</span>
            <div className="input-container" style={{ marginBottom: 0 }}>
                <input type="email" id="email" value={email} style={{ marginBottom: 0 }} onChange={(e) => setEmail(e.target.value)} placeholder="Адрес электронной почты"></input>
                <label>Адрес электронной почты</label>
                <div id="error" className="error-message hidden">
                    <span>
                        <svg aria-hidden="true" fill="currentColor" focusable="false" width="16px" height="16px" viewBox="0 0 24 24" xmlns="https://www.w3.org/2000/svg">
                            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"></path>
                        </svg>
                    </span>
                    <span id="message"></span>
                </div>
            </div>
            <div className="notice">
                <span>Вам нужно будет подтвердить, что это ваш адрес электронной почты</span>
            </div>
            <div className="sign-buttons">
                <button className="left-button" onClick={() => setContainerContent(containerContent - 1)}>Назад</button>
                <button className="right-button" onClick={handleNextButtonClick}>Далее</button>
            </div>
        </>
    );
}

export default Email;