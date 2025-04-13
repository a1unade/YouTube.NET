import PaymentModal from '../../components/modal/payment-modal.tsx';
import { useState } from 'react';
import AddFundsModal from '../../components/modal/addfunds-modal.tsx';

const Premium = (props: { userId: string | null; balanceId: string }) => {
  const { userId, balanceId } = props;
  const [modal, setModal] = useState(false);

  return (
    <div className="premium-page-layout">
      <div className="premium-header-layout">
        <img
          src="https://www.gstatic.com/youtube/img/promos/growth/e7c0850faf8290ead635fb188d20df5095ecb6d518a7d3fbabb42b51e7302330_573x93.png"
          alt="premium logo"
        />
        <h1>YouTube нон-стоп</h1>
        <h3>Смотрите без рекламы, слушайте в фоне и скачивайте видео офлайн</h3>
        <h3>1 месяц всего за 249 ₽ • Отменить можно в любой момент</h3>
        <button className="premium-pay-button" onClick={() => setModal(true)}>
          Попробовать
        </button>
        <h3>Или выберите более выгодный план — 6 месяцев или год</h3>
        <h6 className="premium-notice-section">
          За 7 дней до окончания пробного периода мы напомним. Деньги спишутся автоматически.
        </h6>
      </div>

      <div
        className="premium-header-layout"
        style={{ background: 'transparent', height: 'auto', padding: '50px 0' }}
      >
        <h1>Выберите подходящий тариф</h1>
        <div className="premium-plans-list">
          <div className="premium-plan">
            <div className="plan-header">
              <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24">
                <path d="M12.73 11.93C14.59 11.58 16 9.96 16 8c0-2.21-1.79-4-4-4S8 5.79 8 8c0 1.96 1.41 3.58 3.27 3.93C5.1 12.2 2 15.84 2 20h20c0-4.16-3.1-7.8-9.27-8.07z" />
              </svg>
              <h2>1 месяц</h2>
            </div>
            <div className="premium-plan-details">
              <p>Подписка на месяц</p>
              <h4>249₽ за месяц</h4>
              <h5>249₽/мес далее</h5>
              <span>Автопродление включено</span>
              <ul className="plan-benefits">
                <li>Без рекламы</li>
                <li>Фоновое воспроизведение</li>
                <li>Загрузка видео для просмотра офлайн</li>
              </ul>
            </div>
          </div>

          <div className="premium-plan">
            <div className="plan-header">
              <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24">
                <path d="M12.73 11.93C14.59 11.58 16 9.96 16 8c0-2.21-1.79-4-4-4S8 5.79 8 8c0 1.96 1.41 3.58 3.27 3.93C5.1 12.2 2 15.84 2 20h20c0-4.16-3.1-7.8-9.27-8.07z" />
              </svg>
              <h2>6 месяцев</h2>
            </div>
            <div className="premium-plan-details">
              <p>Полугодовая подписка</p>
              <h4>1349₽ единоразово</h4>
              <h5>Экономия 10%</h5>
              <span>Без автопродления</span>
              <ul className="plan-benefits">
                <li>Больше за меньшие деньги</li>
                <li>Те же Premium-привилегии</li>
                <li>Удобно в подарок</li>
              </ul>
            </div>
          </div>

          <div className="premium-plan">
            <div className="plan-header">
              <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24">
                <path d="M12.73 11.93C14.59 11.58 16 9.96 16 8c0-2.21-1.79-4-4-4S8 5.79 8 8c0 1.96 1.41 3.58 3.27 3.93C5.1 12.2 2 15.84 2 20h20c0-4.16-3.1-7.8-9.27-8.07z" />
              </svg>
              <h2>1 год</h2>
            </div>
            <div className="premium-plan-details">
              <p>Годовая подписка</p>
              <h4>2249₽</h4>
              <h5>Экономия 25%</h5>
              <span>Без автопродления</span>
              <ul className="plan-benefits">
                <li>Максимальная выгода</li>
                <li>Год без рекламы</li>
                <li>Все функции YouTube Premium</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
      <div className="feature-section">
        <div className="feature-content">
          <h2>Смотрите без рекламы</h2>
          <p>
            Получайте максимальное удовольствие от видео без перерывов. Никакой рекламы до, во время
            или после роликов.
          </p>
        </div>
        <div className="feature-image" />
      </div>

      <div className="feature-section reverse">
        <div className="feature-image" />
        <div className="feature-content">
          <h2>Фоновое воспроизведение</h2>
          <p>
            Видео и музыка продолжают играть, даже если вы закрыли приложение или заблокировали
            экран.
          </p>
        </div>
      </div>

      <div className="feature-section">
        <div className="feature-content">
          <h2>Скачивайте и смотрите офлайн</h2>
          <p>
            Скачивайте любимые видео и наслаждайтесь ими в любом месте — даже без подключения к
            интернету.
          </p>
        </div>
        <div className="feature-image" />
      </div>

      <div className="feature-section reverse">
        <div className="feature-image" />
        <div className="feature-content">
          <h2>Режим "Картинка в картинке"</h2>
          <p>Смотрите видео, пока общаетесь в мессенджерах или серфите в других приложениях.</p>
        </div>
      </div>
      <PaymentModal active={modal} setActive={setModal} userId={userId} balanceId={balanceId} />
      <AddFundsModal active={modal} setActive={setModal} userId={userId} balanceId={balanceId} />
    </div>
  );
};

export default Premium;
