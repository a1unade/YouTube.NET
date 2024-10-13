import React, { useState } from 'react';
import errorMessages from '../../../utils/error-messages.ts';
import apiClient from '../../../utils/api-client.ts';
import { useNavigate } from 'react-router-dom';

const Confirmation = (props: {
  email: string;
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
  userId: string;
}) => {
  const { email, setContainerContent, containerContent, userId } = props;
  const navigate = useNavigate();
  const [code, setCode] = useState('');
  const handleNextButtonClick = () => {
    if (code.length < 5) {
      document.getElementById('code')!.classList.add('error', 'shake');
      document.getElementById('code-error')!.classList.remove('hidden');
      document.getElementById('code-message')!.textContent = errorMessages.emptyCode;
      setTimeout(() => {
        document.getElementById('code')!.classList.remove('shake');
      }, 500);
    } else {
      apiClient
        .post('User/CodeCheckForEmail', {
          id: userId,
          code: code,
        })
        .then((response) => {
          if (response.status === 200) {
            setContainerContent(containerContent + 1);
          } else {
            navigate('/error');
          }
        });
    }
  };
  return (
    <>
      <div className="header">
        <h1 style={{ maxWidth: 300 }}>Подтвердите адрес электронной почты</h1>
        <div className="notice" style={{ marginLeft: 0, marginTop: 30, maxWidth: 350 }}>
          <span style={{ fontSize: 14, lineHeight: 1.5 }}>
            Введите код подтверждения, отправленный на адрес {email}. Если письма нет во входящих,
            проверьте папку "Спам".
          </span>
        </div>
      </div>
      <div className="input-container">
        <input
          id="code"
          value={code}
          onChange={(e) => setCode(e.target.value)}
          type="text"
          style={{ marginBottom: 0 }}
          placeholder="Введите код"
        />
        <label>Введите код</label>
        <div id="code-error" className="error-message hidden" style={{ marginTop: 5 }}>
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
          <span id="code-message" />
        </div>
      </div>
      <div className="sign-buttons">
        <button
          className="left-button"
          style={{ minWidth: 'fit-content', padding: 10 }}
          onClick={() => setContainerContent(containerContent - 1)}
        >
          Назад
        </button>
        <button className="right-button" onClick={handleNextButtonClick}>
          Далее
        </button>
      </div>
    </>
  );
};

export default Confirmation;
