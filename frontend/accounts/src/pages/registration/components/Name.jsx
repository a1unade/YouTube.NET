import { validateName, validateSurname } from "../../../utils/validator";
import { Link } from 'react-router-dom';
import {useActions} from "../../../hooks/useActions.js";

// eslint-disable-next-line react/prop-types
const Name = ({ setContainerContent, containerContent, name, setName, surname, setSurname }) => {
    const {updateUserName, updateUserSurname} = useActions();
    const handleNextButtonClick = () => {
        const nameMessage = validateName(name);
        const surnameMessage = validateSurname(surname);
        if (nameMessage.length > 0) {
            document.getElementById("name").classList.add("error", "shake");
            document.getElementById("name-error").classList.remove("hidden");
            document.getElementById("name-message").textContent = nameMessage;
            setTimeout(() => {
                document.getElementById("name").classList.remove("shake");
            }, 500);
        }

        if (surnameMessage.length > 0) {
            document.getElementById("surname").classList.add("error", "shake");
            document.getElementById("surname-error").classList.remove("hidden");
            document.getElementById("surname-message").textContent = surnameMessage;
            setTimeout(() => {
                document.getElementById("surname").classList.remove("shake");
            }, 500);
        }

        if (surnameMessage.length === 0 && nameMessage.length === 0) {
            updateUserName(name);
            // eslint-disable-next-line react/prop-types
            if(surname.length !== 0) updateUserSurname(surname);

            setContainerContent(containerContent + 1);
        }
    }

    return (
        <>
            <h1>Создать аккаунт Google</h1>
            <span>Введите свое имя</span>
            <div className="input-container" style={{ marginBottom: 10 }}>
                <input id="name" type="text" value={name} onChange={(e) => setName(e.target.value)} placeholder="Имя"></input>
                <label>Имя</label>
                <div id="name-error" className="error-message hidden">
                    <span>
                        <svg aria-hidden="true" fill="currentColor" focusable="false" width="16px" height="16px" viewBox="0 0 24 24" xmlns="https://www.w3.org/2000/svg">
                            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"></path>
                        </svg>
                    </span>
                    <span id="name-message"></span>
                </div>
            </div>
            <div className="input-container" style={{ marginTop: 0 }}>
                <input id="surname" type="text" value={surname} onChange={(e) => setSurname(e.target.value)} placeholder="Фамилия (необязательно)"></input>
                <label>Фамилия (необязательно)</label>
                <div id="surname-error" className="error-message hidden">
                    <span>
                        <svg aria-hidden="true" fill="currentColor" focusable="false" width="16px" height="16px" viewBox="0 0 24 24" xmlns="https://www.w3.org/2000/svg">
                            <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"></path>
                        </svg>
                    </span>
                    <span id="surname-message"></span>
                </div>
            </div>
            <div style={{ marginTop: 0 }} className="input-container">
                <span>Уже зарегистрированы? Вы можете выполнить <Link to={'/signin'}><b>вход</b></Link> в аккаунт.</span>
            </div>
            <div className="sign-buttons">
                <div></div>
                <button className="right-button" onClick={handleNextButtonClick}>Далее</button>
            </div>
        </>
    );
}

export default Name;