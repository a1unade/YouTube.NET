import { useNavigate } from 'react-router-dom';
import React from 'react';
import { handleNextButtonClick } from '../../../utils/button-handlers.ts';
import apiClient from '../../../utils/api-client.ts';
import { EmailResponse } from '../../../interfaces/email-response.ts';
import { useErrors } from '../../../hooks/error/use-errors.ts';

const Email = (props: {
  setEmail: React.Dispatch<React.SetStateAction<string>>;
  email: string;
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
}) => {
  const { setContainerContent, containerContent, setEmail, email } = props;
  const navigate = useNavigate();
  const { setErrorAndRedirect } = useErrors();

  const checkUserEmail = () => {
    apiClient
      .post<EmailResponse>('User/CheckUserEmail', {
        email: email,
      })
      .then((response) => {
        const { newUser, confirmation } = response.data;

        if (newUser) {
          setErrorAndRedirect('Аккаунт с таким адресом почты не найден!');
          return;
        }

        if (!confirmation) {
          setErrorAndRedirect('Необходимо подтвердить почту!');
          return;
        }

        if (!newUser && confirmation) {
          setContainerContent(containerContent + 1);
          return;
        }
      })
      .catch((error) => {
        const errorMessage = error.response?.data.Error || null;
        setErrorAndRedirect(errorMessage);
      });
  };

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
            onClick={() => {
              handleNextButtonClick(email);
              checkUserEmail();
              setContainerContent(containerContent + 1);
            }}
          >
            Далее
          </button>
        </div>
      </div>
    </>
  );
};

export default Email;
