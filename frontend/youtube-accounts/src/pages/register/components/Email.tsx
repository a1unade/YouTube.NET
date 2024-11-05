import React from 'react';
import { handleNextButtonClick } from '../../../utils/button-handlers.ts';
import apiClient from '../../../utils/api-client.ts';
import { EmailResponse } from '../../../interfaces/email-response.ts';
import { useNavigate } from 'react-router-dom';
import { useErrors } from '../../../hooks/error/use-errors.ts';

const Email = (props: {
  email: string;
  setEmail: React.Dispatch<React.SetStateAction<string>>;
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
}) => {
  const navigate = useNavigate();
  const { setErrorAndRedirect } = useErrors();
  const { email, setEmail, setContainerContent, containerContent } = props;

  const checkUserEmail = () => {
    apiClient
      .post<EmailResponse>('User/CheckUserEmail', {
        email: email,
      })
      .then((response) => {
        const { newUser, confirmation, error } = response.data;

        if (newUser) setContainerContent(containerContent + 1);

        if (confirmation) setContainerContent(4);

        if (error) navigate('/error');
      })
      .catch((error) => {
        const errorMessage = error.response?.data.Error || null;
        setErrorAndRedirect(errorMessage);
      });
  };

  return (
    <>
      <h1 style={{ marginBottom: 10 }}>Использовать существующий адрес электронной почты</h1>
      <span style={{ fontSize: 17 }}>
        Введите адрес электронной почты, который вы хотите использовать для своего аккаунта Google.
      </span>
      <div className="input-container" style={{ marginBottom: 0 }}>
        <input
          type="email"
          id="email"
          value={email}
          style={{ marginBottom: 0 }}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="Адрес электронной почты"
        />
        <label>Адрес электронной почты</label>
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
      <div className="notice">
        <span>Вам нужно будет подтвердить, что это ваш адрес электронной почты</span>
      </div>
      <div className="sign-buttons">
        <button className="left-button" onClick={() => setContainerContent(containerContent - 1)}>
          Назад
        </button>
        <button
          className="right-button"
          onClick={() => {
            handleNextButtonClick(email);
            checkUserEmail();
          }}
        >
          Далее
        </button>
      </div>
    </>
  );
};

export default Email;
