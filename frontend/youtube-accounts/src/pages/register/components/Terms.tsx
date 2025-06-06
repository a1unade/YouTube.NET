/* istanbul ignore file */

import React, { useEffect } from 'react';
import apiClient from '../../../utils/api-client.ts';

const Terms = (props: {
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  userId: string;
  name: string;
  surname: string;
}) => {
  const { setContainerContent, userId, name, surname } = props;

  useEffect(() => {
    apiClient
      .post('Payment/CreateWallet', {
        name: `${name} ${surname}`,
        userIdPostgres: userId,
        balance: 0,
      })
      .then((res) => {
        if (res.status !== 200) console.error(res.data);
      });
  });

  return (
    <>
      <div className="header">
        <h1>Политика конфиденциальности и Условия использования</h1>
      </div>
      <div className="terms">
        <span>
          Чтобы создать аккаунт, вам необходимо
          <br /> принять
        </span>
        <a href="https://policies.google.com/terms?hl=ru&gl=RU"> Условия использования</a>
        <span>, приведенные ниже.</span>
      </div>
      <div className="terms">
        <span>
          Кроме того, при создании аккаунта мы обрабатываем ваши данные в соответствии с<br />
        </span>
        <b>
          <a href="https://policies.google.com/privacy?hl=ru&gl=RU">
            {' '}
            Политикой конфиденциальности
          </a>
        </b>
        <span>. Вот ее основные положения:</span>
      </div>
      <div className="terms">
        <h1>Какие данные мы используем</h1>
        <ul>
          <li>
            Мы сохраняем ваши личные данные, указанные при настройке аккаунта Google (например, имя,
            адрес электронной почты и номер телефона).
          </li>
          <li>
            Когда вы оставляете комментарий к ролику на YouTube, мы сохраняем введенные вами данные.
          </li>
          <li>
            Когда вы смотрите видео на YouTube, мы обрабатываем полученную информацию: сведения о
            видео, файлы cookie и геоданные.
          </li>
          <li>Мы также собираем данные, когда вы используете видеоплеер YouTube.</li>
        </ul>
      </div>
      <div className="terms">
        <h1 style={{ marginBottom: 10 }}>Для чего нам нужны данные</h1>
        <span>Мы используем собранные данные только в целях, указанных в нашей</span>
        <b>
          <a href="https://policies.google.com/privacy?hl=ru&gl=RU"> Политике конфиденциальности</a>
        </b>
        <span>, например для того, чтобы:</span>
        <ul>
          <li>Развивать существующие сервисы и создавать новые.</li>
          <li>
            Обеспечивать вашу безопасность, защищая от мошенничества и других противоправных
            действий.
          </li>
          <li>Анализировать работу наших сервисов.</li>
        </ul>
      </div>
      <div className="sign-buttons">
        <button className="left-button" onClick={() => setContainerContent(0)}>
          Отмена
        </button>
        <button
          className="right-button"
          onClick={() => (window.location.href = `http://localhost:5172/auth/${userId}`)}
        >
          Принимаю
        </button>
      </div>
    </>
  );
};

export default Terms;
