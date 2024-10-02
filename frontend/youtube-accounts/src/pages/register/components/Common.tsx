import React, { useState } from 'react';
import { validateBirthDate } from '../../../utils/validator';
import errors from '../../../utils/error-messages.ts';

const Common = (props: {
  setContainerContent: React.Dispatch<React.SetStateAction<number>>;
  containerContent: number;
  gender: string;
  setGender: React.Dispatch<React.SetStateAction<string>>;
}) => {
  const { setContainerContent, gender, setGender, containerContent } = props;
  const [day, setDay] = useState(''); // день рождения
  const [month, setMonth] = useState(''); // месяц рождения
  const [year, setYear] = useState(''); // год рождения

  const handleNextButtonClick = () => {
    const dateMessage = validateBirthDate(year, month, day);

    if (dateMessage.length > 0) {
      document.getElementById('day')!.classList.add('error', 'shake');
      document.getElementById('month')!.classList.add('error', 'shake');
      document.getElementById('year')!.classList.add('error', 'shake');
      document.getElementById('date-error')!.classList.remove('hidden');
      document.getElementById('date-message')!.textContent = dateMessage;
      setTimeout(() => {
        document.getElementById('day')!.classList.remove('shake');
        document.getElementById('month')!.classList.remove('shake');
        document.getElementById('year')!.classList.remove('shake');
      }, 500);
    }

    if (gender.length === 0) {
      document.getElementById('gender')!.classList.add('error', 'shake');
      document.getElementById('gender-error')!.classList.remove('hidden');
      document.getElementById('gender-message')!.textContent = errors.emptyGender;
      setTimeout(() => {
        document.getElementById('gender')!.classList.remove('shake');
      }, 500);
    }

    if (gender.length > 0 && dateMessage.length === 0) {
      setContainerContent(containerContent + 1);
    }
  };

  return (
    <>
      <h1>Общие сведения</h1>
      <span>Укажите свою дату рождения и пол</span>
      <div className="input-container">
        <div className="birth-date">
          <div className="input-container" style={{ margin: 0 }}>
            <input
              type="tel"
              value={day}
              id="day"
              maxLength={2}
              placeholder="День"
              onChange={(e) => setDay(e.target.value)}
              style={{ marginLeft: 0 }}
            />
            <label>День</label>
          </div>
          <div className="input-container" style={{ margin: 0 }}>
            <select value={month} id="month" onChange={(e) => setMonth(e.target.value)} required>
              <option value="" disabled hidden>
                Месяц
              </option>
              <option value="1">Январь</option>
              <option value="2">Февраль</option>
              <option value="3">Март</option>
              <option value="4">Апрель</option>
              <option value="5">Май</option>
              <option value="6">Июнь</option>
              <option value="7">Июль</option>
              <option value="8">Август</option>
              <option value="9">Сентябрь</option>
              <option value="10">Октябрь</option>
              <option value="11">Ноябрь</option>
              <option value="12">Декабрь</option>
            </select>
            <label style={{ marginLeft: 10 }}>Месяц</label>
          </div>
          <div className="input-container" style={{ margin: 0 }}>
            <input
              type="tel"
              value={year}
              id="year"
              onChange={(e) => setYear(e.target.value)}
              maxLength={4}
              placeholder="Год"
            />
            <label style={{ marginLeft: 20 }}>Год</label>
          </div>
        </div>
        <div id="date-error" className="error-message hidden">
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
          <span id="date-message" />
        </div>
      </div>
      <div className="input-container" style={{ marginTop: 10 }}>
        <select id="gender" value={gender} onChange={(e) => setGender(e.target.value)} required>
          <option value="" disabled hidden>
            Пол
          </option>
          <option value="ж">Жен.</option>
          <option value="м">Муж.</option>
          <option value="-">Не указан</option>
        </select>
        <label>Пол</label>
        <div id="gender-error" className="error-message hidden">
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
          <span id="gender-message" />
        </div>
        <a href="https://support.google.com/accounts/answer/1733224?hl=ru">
          <b>Почему мы просим указать дату рождения и пол</b>
        </a>
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

export default Common;
