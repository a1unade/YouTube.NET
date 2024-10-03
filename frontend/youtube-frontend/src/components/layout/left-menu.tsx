/* istanbul ignore file */
import MenuButton from './menu-button.tsx';
import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

const LeftMenu = (props: { isOpen: boolean }) => {
  const { isOpen } = props;
  const navigate = useNavigate();
  const location = useLocation();
  const mapRouting: { [p: string]: string } = {
    '/': 'Главная',
    '/feed/subscriptions': 'Подписки',
    '/feed/playlists': 'Плейлисты',
  };

  const [selected, setSelected] = useState(mapRouting[location.pathname] || '');

  const handleClick = (title: string) => {
    const prevElement = document.getElementById(`button-${selected}`);
    if (prevElement) {
      prevElement.classList.remove('menu-button-selected');
    }
    setSelected(title);
    const currentElement = document.getElementById(`button-${title}`);
    if (currentElement) {
      currentElement.classList.add('menu-button-selected');
    }
  };

  return (
    <>
      {isOpen ? (
        <div className="left-menu">
          <div className="sidebar-container">
            <div className="first-container">
              <MenuButton
                onClick={() => {
                  handleClick('Главная');
                  navigate('/');
                }}
                selected={selected}
                title="Главная"
              />
              <MenuButton
                onClick={() => {
                  handleClick('Подписки');
                  navigate('/feed/subscriptions');
                }}
                selected={selected}
                title="Подписки"
              />
            </div>
            <>
              <div className="first-container">
                <div className="best-youtube-section-title-container">
                  <p className="youtube-title-style">Вы</p>
                </div>
                <MenuButton
                  onClick={() => handleClick('Мой канал')}
                  selected={selected}
                  title="Мой канал"
                />
                <MenuButton
                  onClick={() => handleClick('История')}
                  selected={selected}
                  title="История"
                />
                <MenuButton
                  onClick={() => {
                    handleClick('Плейлисты');
                    navigate('/feed/playlists');
                  }}
                  selected={selected}
                  title="Плейлисты"
                />
                <MenuButton
                  onClick={() => handleClick('Ваши видео')}
                  selected={selected}
                  title="Ваши видео"
                />
                <MenuButton
                  onClick={() => handleClick('Смотреть позже')}
                  selected={selected}
                  title="Смотреть позже"
                />
                <MenuButton
                  onClick={() => handleClick('Понравившиеся')}
                  selected={selected}
                  title="Понравившиеся"
                />
              </div>
              <div className="first-container">
                <div className="best-youtube-section-title-container">
                  <p className="youtube-title-style">Подписки</p>
                </div>
                <button className="sidebar-button">
                  <div className="svg-container">
                    <img
                      src="https://sm.ign.com/ign_nordic/cover/a/avatar-gen/avatar-generations_prsz.jpg"
                      alt=""
                    />
                  </div>
                  <p className="sidebar-text">Clever Monkey</p>
                </button>
              </div>
            </>
            <div className="first-container">
              <div className="best-youtube-section-title-container">
                <p className="youtube-title-style">Навигатор</p>
              </div>
              <MenuButton
                onClick={() => handleClick('В тренде')}
                selected={selected}
                title="В тренде"
              />
              <MenuButton
                onClick={() => handleClick('Музыка')}
                selected={selected}
                title="Музыка"
              />
              <MenuButton
                onClick={() => handleClick('Фильмы')}
                selected={selected}
                title="Фильмы"
              />
              <MenuButton
                onClick={() => handleClick('Видеоигры')}
                selected={selected}
                title="Видеоигры"
              />
              <MenuButton onClick={() => handleClick('Спорт')} selected={selected} title="Спорт" />
            </div>
            <div className="first-container">
              <div className="best-youtube-section-title-container">
                <p className="youtube-title-style">Другие возможности</p>
              </div>
              <MenuButton
                onClick={() => handleClick('Творческая студия')}
                selected={selected}
                title="Творческая студия"
              />
              <MenuButton
                onClick={() => handleClick('YouTube Premium')}
                selected={selected}
                title="YouTube Premium"
              />
            </div>
            <div className="first-container">
              <div className="best-youtube-section-title-container">
                <p className="youtube-title-style" />
              </div>
              <MenuButton
                onClick={() => handleClick('Настройки')}
                selected={selected}
                title="Настройки"
              />
              <MenuButton
                onClick={() => handleClick('Жалобы')}
                selected={selected}
                title="Жалобы"
              />
              <MenuButton
                onClick={() => handleClick('Справка')}
                selected={selected}
                title="Справка"
              />
              <MenuButton
                onClick={() => handleClick('Отправить отзыв')}
                selected={selected}
                title="Отправить отзыв"
              />
            </div>
            <div className="content-section-with-links-and-copyright">
              <a href="https://www.youtube.com/about/">О сервисе</a>
              <a href="https://www.youtube.com/about/press/">Прессе</a>
              <a href="https://www.youtube.com/about/copyright/">Авторские права</a>
              <a href="https://www.youtube.com/t/contact_us/">Связаться с нами</a>
              <a href="https://www.youtube.com/creators/">Авторам</a>
              <a href="https://www.youtube.com/ads/">Рекламодателям</a>
              <a href="https://developers.google.com/youtube">Разработчикам</a>
            </div>
            <div className="content-section-with-links-and-copyright">
              <a href="https://www.youtube.com/t/terms">Условия использования</a>
              <a href="https://www.youtube.com/t/privacy">Конфиденциальность</a>
              <a href="https://www.youtube.com/about/policies/">Правила и безопасность</a>
              <a href="https://www.youtube.com/howyoutubeworks?utm_campaign=ytgen&utm_source=ythp&utm_medium=LeftNav&utm_content=txt&u=https%3A%2F%2Fwww.youtube.com%2Fhowyoutubeworks%3Futm_source%3Dythp%26utm_medium%3DLeftNav%26utm_campaign%3Dytgen">
                Как работает YouTube
              </a>
              <a href="https://www.youtube.com/new">Тестирование новых функций</a>
              <p className="copyright-text-styling">© 2024 Google LLC Youtube, компания Google</p>
            </div>
          </div>
        </div>
      ) : (
        <div style={{ width: 60 }} className="left-menu closed-menu">
          <div style={{ marginTop: 20 }}>
            <a href="/">
              <MenuButton
                onClick={() => handleClick('Главная')}
                selected={selected}
                title="Главная"
              />
            </a>
            <MenuButton
              onClick={() => handleClick('Подписки')}
              selected={selected}
              title="Подписки"
            />
            <MenuButton onClick={() => handleClick('Вы')} selected={selected} title="Вы" />
          </div>
        </div>
      )}
    </>
  );
};

export default LeftMenu;
