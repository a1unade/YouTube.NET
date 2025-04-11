const Premium = () => {
  return (
    <>
      <div className="premium-page-layout">
        <div className="premium-header-layout">
          <img
            src="https://www.gstatic.com/youtube/img/promos/growth/e7c0850faf8290ead635fb188d20df5095ecb6d518a7d3fbabb42b51e7302330_573x93.png"
            alt="premium logo"
          />
          <h1>YouTube нон-стоп</h1>
          <h3>YouTube без рекламы, в фоновом режиме и офлайн</h3>
          <h3>
            1 пробный месяц бесплатно, затем 249 ₽/месяц • Отказаться от подписки можно в любое
            время.
          </h3>
          <button className="premium-pay-button">1 месяц за 0₽</button>
          <h3>Вы также можете оформить подписку на 6 месяцев или на 1 год – это очень выгодно.</h3>
          <h6 className="premium-notice-section">
            За 7 дней до окончания периода мы отправим вам напоминание. Взимается абонентская плата.
          </h6>
        </div>
        <div className="premium-header-layout" style={{ background: 'transparent' }}>
          <h1>Выберите план</h1>
          <div className="premium-plans-list">
            <div className="premium-plan">
              <div className="plan-header">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="24"
                  viewBox="0 0 24 24"
                  width="24"
                  focusable="false"
                  aria-hidden="true"
                >
                  <path d="M12.73 11.93C14.59 11.58 16 9.96 16 8c0-2.21-1.79-4-4-4S8 5.79 8 8c0 1.96 1.41 3.58 3.27 3.93C5.1 12.2 2 15.84 2 20h20c0-4.16-3.1-7.8-9.27-8.07zM9 8c0-1.65 1.35-3 3-3s3 1.35 3 3-1.35 3-3 3-3-1.35-3-3zm3 4.9c5.98 0 8.48 3.09 8.93 6.1H3.07c.45-3.01 2.95-6.1 8.93-6.1z" />
                </svg>
                <h2>1 месяц</h2>
              </div>
            </div>
            <div className="premium-plan">
              <div className="plan-header">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="24"
                  viewBox="0 0 24 24"
                  width="24"
                  focusable="false"
                  aria-hidden="true"
                >
                  <path d="M12.73 11.93C14.59 11.58 16 9.96 16 8c0-2.21-1.79-4-4-4S8 5.79 8 8c0 1.96 1.41 3.58 3.27 3.93C5.1 12.2 2 15.84 2 20h20c0-4.16-3.1-7.8-9.27-8.07zM9 8c0-1.65 1.35-3 3-3s3 1.35 3 3-1.35 3-3 3-3-1.35-3-3zm3 4.9c5.98 0 8.48 3.09 8.93 6.1H3.07c.45-3.01 2.95-6.1 8.93-6.1z" />
                </svg>
                <h2>6 месяцев</h2>
              </div>
            </div>
            <div className="premium-plan">
              <div className="plan-header">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="24"
                  viewBox="0 0 24 24"
                  width="24"
                  focusable="false"
                  aria-hidden="true"
                >
                  <path d="M12.73 11.93C14.59 11.58 16 9.96 16 8c0-2.21-1.79-4-4-4S8 5.79 8 8c0 1.96 1.41 3.58 3.27 3.93C5.1 12.2 2 15.84 2 20h20c0-4.16-3.1-7.8-9.27-8.07zM9 8c0-1.65 1.35-3 3-3s3 1.35 3 3-1.35 3-3 3-3-1.35-3-3zm3 4.9c5.98 0 8.48 3.09 8.93 6.1H3.07c.45-3.01 2.95-6.1 8.93-6.1z" />
                </svg>
                <h2>1 год</h2>
              </div>
              <div className="premium-plan-details">
                <p>Ежегодно</p>
                <h4>12 месяцев за 2249₽</h4>
                <h5>На 25% выгоднее ежемесячного тарифа</h5>
                <span>Без автоматического продления</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Premium;
