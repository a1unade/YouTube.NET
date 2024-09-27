import React from 'react';

const Check = (props: {
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
  name: string;
  email: string;
}) => {
  const { setContainerContent, containerContent, name, email } = props;
  return (
    <>
      <div className="header">
        <h1>Проверьте данные</h1>
        <div style={{ maxWidth: 300 }}>
          <span>Вы можете использовать этот адрес электронной почты для входа в аккаунт.</span>
        </div>
      </div>
      <div className="user" style={{ justifyContent: 'flex-start' }}>
        <div className="user-image">
          <img src="https://avatars.githubusercontent.com/u/113981832?v=4" alt="img" />
        </div>
        <div className="user-info">
          <p>
            <b>{name}</b>
          </p>
          <span>{email}</span>
        </div>
      </div>
      <div className="sign-buttons">
        <button className="left-button" onClick={() => setContainerContent(containerContent - 1)}>
          Назад
        </button>
        <button className="right-button" onClick={() => setContainerContent(containerContent + 1)}>
          Далее
        </button>
      </div>
    </>
  );
};

export default Check;
