import React from "react";

const Password = ({ setContainerContent, containerContent, setPassword, email, password }) => {
    const makePasswordVisibile = () => {
        const passwordInput = document.getElementById("password");
        const showPasswordButton = document.getElementById("showPasswordButton");

        if (passwordInput.type === "password") {
            passwordInput.type = "text";
            showPasswordButton.textContent = "Скрыть пароль";
        } else {
            passwordInput.type = "password";
            showPasswordButton.textContent = "Показать пароль";
        }
    }

    return (
        <>
            <h1>Добро пожаловать!</h1>
            <div className="user">
                <div className="user-email">
                    <svg aria-hidden="true" fill="currentColor" focusable="false" width="24px" height="24px" viewBox="0 0 24 24" xmlns="https://www.w3.org/2000/svg">
                        <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm6.36 14.83c-1.43-1.74-4.9-2.33-6.36-2.33s-4.93.59-6.36 2.33C4.62 15.49 4 13.82 4 12c0-4.41 3.59-8 8-8s8 3.59 8 8c0 1.82-.62 3.49-1.64 4.83zM12 6c-1.94 0-3.5 1.56-3.5 3.5S10.06 13 12 13s3.5-1.56 3.5-3.5S13.94 6 12 6z"></path>
                    </svg>
                    <h1>{email}</h1>
                </div>
            </div>
            <div className="input-container">
                <span>Сначала подтвердите, что это ваш аккаунт.</span>
            </div>
            <div className="input-container" style={{ marginBottom: 10 }}>
                <input type="password" id="password" value={password} autoComplete="off" onChange={(e) => setPassword(e.target.value)} placeholder="Введите пароль"></input>
                <label>Введите пароль</label>
            </div>
            <div className="sign-buttons" style={{ marginTop: 10 }}>
                <button className="password-button" id="showPasswordButton" onClick={makePasswordVisibile}>Показать пароль</button>
            </div>
            <div className="sign-buttons">
                <button className="left-button" onClick={() => setContainerContent(containerContent - 1)}>Сменить аккаунт</button>
                <button className="right-button">Далее</button>
            </div>
        </>
    );
}

export default Password;