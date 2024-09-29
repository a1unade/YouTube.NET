import MenuButton from './components/MenuButton.jsx'
import '../../../assets/styles/left-menu.css';
import {useSelector} from "react-redux";

// eslint-disable-next-line react/prop-types
function LeftMenu({isOpen}) {
    const {userId} = useSelector((state) => state.user.userId);

    return (
        <>
            {isOpen ?
                <div className='left-menu'>
                    <div className='sidebar-container'>
                        <div className='first-container'>
                            <a href="/">
                                <MenuButton title={'Главная'}/>
                            </a>
                            {userId !== "" ? <MenuButton title={'Подписки'}/> : null}
                        </div>
                        {
                            userId !== "" ?
                                <>
                                    <div className='first-container'>
                                        <div className='best-youtube-section-title-container'>
                                            <p className='youtube-title-style'>Вы</p>
                                        </div>
                                        <MenuButton title={'Мой канал'}/>
                                        <MenuButton title={'Ваши видео'}/>
                                    </div>
                                    <div className='first-container'>
                                        <div className='best-youtube-section-title-container'>
                                            <p className='youtube-title-style'>Подписки</p>
                                        </div>
                                        <button className='sidebar-button'>
                                            <div className='svg-container'>
                                                <img
                                                    src='https://sm.ign.com/ign_nordic/cover/a/avatar-gen/avatar-generations_prsz.jpg'
                                                    alt=''/>
                                            </div>
                                            <p className='sidebar-text'>Clever Monkey</p>
                                        </button>
                                    </div>
                                </>
                                :
                                null
                        }
                        <div className='first-container'>
                            <div className='best-youtube-section-title-container'>
                                <p className='youtube-title-style'>Навигатор</p>
                            </div>
                            <a href="/feed/trending/news"><MenuButton title={'В тренде'}/>
                            </a>
                            <a href="/channel/Music"> <MenuButton title={'Музыка'}/></a>
                            <a href="/feed"><MenuButton title={'Фильмы'}/></a>
                            <a href="/channel/VideoGames"><MenuButton title={'Видеоигры'}/></a>
                            <a href="/channel/Sport"><MenuButton title={'Спорт'}/></a>
                        </div>
                        {
                            userId !== "" ?
                                <div className='first-container'>
                                    <div className='best-youtube-section-title-container'>
                                        <p className='youtube-title-style'>Другие возможности</p>
                                    </div>
                                    <a href="/channel/edit/1"><MenuButton title={'Творческая студия'}/></a>
                                    <MenuButton title={'YouTube Premium'}/>
                                </div>
                                :
                                null
                        }
                        <div className='content-section-with-links-and-copyright'>
                            <a href='https://www.youtube.com/about/'>О сервисе</a>
                            <a href='https://www.youtube.com/about/press/'>Прессе</a>
                            <a href='https://www.youtube.com/about/copyright/'>Авторские права</a>
                            <a href='https://www.youtube.com/t/contact_us/'>Связаться с нами</a>
                            <a href='https://www.youtube.com/creators/'>Авторам</a>
                            <a href='https://www.youtube.com/ads/'>Рекламодателям</a>
                            <a href='https://developers.google.com/youtube'>Разработчикам</a>
                        </div>
                        <div className='content-section-with-links-and-copyright'>
                            <a href='https://www.youtube.com/t/terms'>Условия использования</a>
                            <a href='https://www.youtube.com/t/privacy'>Конфиденциальность</a>
                            <a href='https://www.youtube.com/about/policies/'>Правила и безопасность</a>
                            <a href='https://www.youtube.com/howyoutubeworks?utm_campaign=ytgen&utm_source=ythp&utm_medium=LeftNav&utm_content=txt&u=https%3A%2F%2Fwww.youtube.com%2Fhowyoutubeworks%3Futm_source%3Dythp%26utm_medium%3DLeftNav%26utm_campaign%3Dytgen'>Как
                                работает YouTube</a>
                            <a href='https://www.youtube.com/new'>Тестирование новых функций</a>
                            <p className='copyright-text-styling'>© 2024 Google LLC Youtube, компания Google</p>
                        </div>
                    </div>
                </div> :
                <div style={{width: 60}} className='left-menu closed-menu'>
                    <a href="/">
                        <MenuButton title={'Главная'}/>
                    </a>
                    {
                        userId !== "" ?
                            <>
                                <MenuButton title={'Подписки'}/>
                                <MenuButton title={'Вы'}/>
                            </>
                            :
                            null
                    }
                    <script
                        src="https://yastatic.net/s3/passport-sdk/autofill/v1/sdk-suggest-with-polyfills-latest.js"></script>

                </div>
            }
        </>
    );
}

export default LeftMenu;