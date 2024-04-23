import './style.css';

const MainContent = () => {
    return (
        <div className="account-block">
            <h2 className="block-title">Аккаунт</h2>

            <div className="user-info">
                <div>
                    <p className="info-header">Добавьте сведения о себе и настройте доступ к функциям YouTube</p>
                    <p className="info-email">Вы вошли в аккаунт Val1lazar66@gmail.com</p>
                </div>
                <img className="user-avatar" src="/path/to/avatar.png" alt="Аватар пользователя" />
            </div>
            <hr className="divider" />

            {/* Заголовок "Ваш канал YouTube" */}
            <h3 className="channel-title">Ваш канал YouTube</h3>
            <p className="channel-info">Чтобы загружать ролики, оставлять комментарии, создавать плейлисты и пользоваться другими функциями YouTube, нужно создать канал.</p>

            {/* Блок с информацией о канале пользователя */}
            <div className="channel-info">
                <div className="channel-name-container">
                    <p className="channel-name-label">Мой канал</p>
                </div>
                <div className="channel-details">
                    <div className="channel-avatar-name">
                        <img className="channel-avatar" src='https://sm.ign.com/ign_nordic/cover/a/avatar-gen/avatar-generations_prsz.jpg' alt="Аватар канала" />
                        <p className="channel-name">Название канала</p>
                    </div>
                    <div className="studio-container">
                        <a href="/path/to/creative-studio" className="studio-label">Творческая студия</a>
                    </div>
                </div>
            </div>
            <hr className="divider" />

            {/* Информация о подписке */}
            <h3 className="subscription-title">Ваш аккаунт</h3>
            <p className="subscription-info">Для входа на YouTube используется аккаунт Google.</p>

            <div className="channel-info">
                <div className="channel-name-container">
                    <p className="channel-name-label">Подписка</p>
                </div>
                <div className="subscription-details">
                    <div className="text-container">
                        <p className="channel-name-label">Подписка YouTube Premium не оформлена. | <a href="" className="studio-label">Оформить YouTube Premium</a></p>
                        <p className="text-name-label">С подпиской YouTube Premium вас ждет музыка в фоновом режиме, видео без рекламы и многое другое.</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default MainContent;