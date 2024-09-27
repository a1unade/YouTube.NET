import React from 'react';

const Confirmation = (props: {
  email: string;
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
}) => {
  const { email, setContainerContent, containerContent } = props;

  return (
    <>
      <div className="header">
        <h1 style={{ maxWidth: 300 }}>Подтвердите адрес электронной почты</h1>
        <div className="notice" style={{ marginLeft: 0, marginTop: 30, maxWidth: 350 }}>
          <span
            style={{
              fontSize: 14,
              lineHeight: 1.5,
            }}
          >
            Следйуте инструкции в письме, отправленном на адрес {email}. Если письма нет во
            входящих, проверьте папку "Спам".
          </span>
        </div>
      </div>
      <button onClick={() => setContainerContent(containerContent + 1)}>Next</button>
    </>
  );
};

export default Confirmation;
