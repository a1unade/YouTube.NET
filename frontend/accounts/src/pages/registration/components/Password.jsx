import { useState } from "react";
import { validatePassword } from "../../../utils/validator";
import errors from "../../../utils/errorMessages";
import {useSelector} from "react-redux";
import {apiClient} from "../../../utils/apiClient.js";
import {useNavigate} from "react-router-dom";
import {useActions} from "../../../hooks/useActions.js";

// eslint-disable-next-line react/prop-types
const Password = ({ setContainerContent, containerContent }) => {
    const [password, setPassword] = useState('');
    const [confirm, setConfirm] = useState('');
    const {email} = useSelector(state => state.email);
    const {name} = useSelector(state => state.name);
    const {surname} = useSelector(state => state.surname);
    const {birthdate} = useSelector(state => state.birthdate);
    const {gender} = useSelector(state => state.gender);
    const navigate = useNavigate();
    const {updateUserId} = useActions();

    const makePasswordVisible = () => {
        const passwordInput = document.getElementById("password");
        const confirmInput = document.getElementById("confirm");
        const showPasswordButton = document.getElementById("showPasswordButton");

        if (passwordInput.type === "password") {
            passwordInput.type = "text";
            confirmInput.type = "text";
            showPasswordButton.textContent = "Скрыть пароль";
        } else {
            passwordInput.type = "password";
            confirmInput.type = "password";
            showPasswordButton.textContent = "Показать пароль";
        }
    }

    const register = async () => {
        try {
            const response = await apiClient.post('Auth/register', {
                    email,
                    password,
                    name,
                    surname,
                    birthdate,
                    gender
                });
            if(response.data.type === 0){
                updateUserId(response.data.userId);
                setContainerContent(containerContent + 1);
            }
            else{
                navigate("/error");
            }
            console.log(response.data);
        } catch (error) {
            console.error('Ошибка при регистрации:', error);
            navigate('/error');
        }
    }

    const handleNextButtonClick = async () => {
        const message = validatePassword(password);
        if (message.length > 0) {
            document.getElementById("password").classList.add("error", "shake");
            document.getElementById("password-error").classList.remove("hidden");
            document.getElementById("password-message").textContent = message;
            setTimeout(() => {
                document.getElementById("password").classList.remove("shake");
            }, 500);
        }

        if (password !== confirm) {
            document.getElementById("password").classList.add("error", "shake");
            document.getElementById("confirm").classList.add("error", "shake");
            document.getElementById("confirm-error").classList.remove("hidden");
            document.getElementById("confirm-message").textContent = errors.passwordConfirm;
            setTimeout(() => {
                document.getElementById("confirm").classList.remove("shake");
            }, 500);
        }

        if (message.length === 0 && password === confirm) {
            await register();
        }
    }

    return (
        <>
            <h1>Создайте надежный пароль</h1>
            <span>Придумайте надежный пароль, состоящий из букв, цифр и других символов</span>
            <div className="input-container" style={{ marginBottom: 20 }}>
                <input id="password" value={password} onChange={(e) => setPassword(e.target.value)} type="password" placeholder="Пароль"></input>
                <label>Пароль</label>
                <div id="password-error" className="error-message hidden">
                    <span>
                        <svg aria-hidden="true" fill="currentColor" focusable="false" width="16px" height="16px" viewBox="0 0 24 24" xmlns="https://www.w3.org/2000/svg">
                            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"></path>
                        </svg>
                    </span>
                    <span id="password-message"></span>
                </div>
            </div>
            <div className="input-container" style={{ marginTop: 0 }}>
                <input id="confirm" value={confirm} onChange={(e) => setConfirm(e.target.value)} type="password" placeholder="Подтвердить"></input>
                <label>Подтвердить</label>
                <div id="confirm-error" className="error-message hidden">
                    <span>
                        <svg aria-hidden="true" fill="currentColor" focusable="false" width="16px" height="16px" viewBox="0 0 24 24" xmlns="https://www.w3.org/2000/svg">
                            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"></path>
                        </svg>
                    </span>
                    <span id="confirm-message"></span>
                </div>
            </div>
            <div className="sign-buttons" style={{ marginTop: 10 }}>
                <button className="password-button" id="showPasswordButton" onClick={makePasswordVisible}>Показать пароль</button>
            </div>
            <div className="sign-buttons">
                <button className="left-button" onClick={() => setContainerContent(containerContent - 1)}>Назад</button>
                <button className="right-button" onClick={handleNextButtonClick}>Далее</button>
            </div>
        </>
    );
}

export default Password;