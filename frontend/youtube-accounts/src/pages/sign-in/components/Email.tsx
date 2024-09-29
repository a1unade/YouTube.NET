import { useNavigate } from 'react-router-dom';
import React from 'react';
import { handleNextButtonClick } from '../../../utils/button-handlers.ts';

const Email = (props: {
  setEmail: React.Dispatch<React.SetStateAction<string>>;
  email: string;
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
}) => {
  const { setContainerContent, containerContent, setEmail, email } = props;
  const navigate = useNavigate();

  return (
    <>
      <h1>Вход</h1>
      <span>Перейдите на YouTube</span>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 80 }}>
        <div className="input-container">
          <input
            type="email"
            value={email}
            id="email"
            placeholder="Адрес эл. почты"
            onChange={(e) => setEmail(e.target.value)}
          />
          <label>Адрес эл. почты</label>
          <div id="error" className="error-message hidden">
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
            <span id="message" />
          </div>
        </div>
        <div className="sign-buttons">
          <button className="left-button" onClick={() => navigate('/signup')}>
            Создать аккаунт
          </button>
          <button
            className="right-button"
            onClick={() => handleNextButtonClick(email, setContainerContent, containerContent)}
          >
            Далее
          </button>
        </div>
      </div>
    </>
  );
};

export default Email;