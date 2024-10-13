import React, { useState } from 'react';
import { validatePassword } from '../../../utils/validator';
import errors from '../../../utils/error-messages.ts';
import { makePasswordVisible } from '../../../utils/button-handlers.ts';

const Password = (props: {
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
  password: string;
  setPassword: React.Dispatch<React.SetStateAction<string>>;
  processAuth: () => void;
}) => {
  const { setContainerContent, containerContent, setPassword, password, processAuth } = props;
  const [confirm, setConfirm] = useState('');

  const handleNextButtonClick = async () => {
    const message = validatePassword(password);
    if (message.length > 0) {
      document.getElementById('password')!.classList.add('error', 'shake');
      document.getElementById('password-error')!.classList.remove('hidden');
      document.getElementById('password-message')!.textContent = message;
      setTimeout(() => {
        document.getElementById('password')!.classList.remove('shake');
      }, 500);
    }

    if (password !== confirm) {
      document.getElementById('password')!.classList.add('error', 'shake');
      document.getElementById('confirm')!.classList.add('error', 'shake');
      document.getElementById('confirm-error')!.classList.remove('hidden');
      document.getElementById('confirm-message')!.textContent = errors.passwordConfirm;
      setTimeout(() => {
        document.getElementById('confirm')!.classList.remove('shake');
      }, 500);
    }

    if (message.length === 0 && password === confirm) {
      processAuth();
    }
  };

  return (
    <>
      <h1>Создайте надежный пароль</h1>
      <span>Придумайте надежный пароль, состоящий из букв, цифр и других символов</span>
      <div className="input-container" style={{ marginBottom: 20 }}>
        <input
          id="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          type="password"
          placeholder="Пароль"
        />
        <label>Пароль</label>
        <div id="password-error" className="error-message hidden">
          <span>
            <svg
              aria-hidden="true"
              fill="currentColor"
              focusable="false"
              width="16px"
              height="16px"
              viewBox="0 0 24 24"
              xmlns="https://www.w3.org/2000/svg"
            >
              <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z" />
            </svg>
          </span>
          <span id="password-message" />
        </div>
      </div>
      <div className="input-container" style={{ marginTop: 0 }}>
        <input
          id="confirm"
          value={confirm}
          onChange={(e) => setConfirm(e.target.value)}
          type="password"
          placeholder="Подтвердить"
        />
        <label>Подтвердить</label>
        <div id="confirm-error" className="error-message hidden">
          <span>
            <svg
              aria-hidden="true"
              fill="currentColor"
              focusable="false"
              width="16px"
              height="16px"
              viewBox="0 0 24 24"
              xmlns="https://www.w3.org/2000/svg"
            >
              <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z" />
            </svg>
          </span>
          <span id="confirm-message" />
        </div>
      </div>
      <div className="sign-buttons" style={{ marginTop: 10 }}>
        <button className="password-button" id="showPasswordButton" onClick={makePasswordVisible}>
          Показать пароль
        </button>
      </div>
      <div className="sign-buttons">
        <button className="left-button" onClick={() => setContainerContent(containerContent - 1)}>
          Назад
        </button>
        <button className="right-button" onClick={handleNextButtonClick}>
          Далее
        </button>
      </div>
    </>
  );
};

export default Password;
