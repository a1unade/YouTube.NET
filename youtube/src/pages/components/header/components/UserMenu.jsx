import {Exit, StudioMenu, Payments, SettingsIcon, HelpIcon, AlertIcon, UserData} from "../../../../assets/Icons";

const UserMenu = () => {
    return (
        <>
            <div className='dropdown-content'>
                <div className='dropdown-section'>
                    <div className="user-menu">
                        <div className="user-menu-image">
                            <img src="https://avatars.githubusercontent.com/u/113981832?v=4"></img>
                        </div>
                        <div className="user-menu-info">
                            <p><b>name</b></p>
                            <span>@username</span>
                            <a style={{marginTop: 10}} href="">Посмотреть канал</a>
                        </div>
                    </div>
                </div>
                <div className='dropdown-section'>
                    <button className='sidebar-button'>
                        <div className='svg-container'>
                            <Exit/>
                        </div>
                        <p className='sidebar-text'>Выйти</p>
                    </button>
                </div>
                <div className='dropdown-section'>
                    <button className='sidebar-button'>
                        <div className='svg-container'>
                            <StudioMenu/>
                        </div>
                        <p className='sidebar-text'>Творческая студия YouTube</p>
                    </button>
                    <button className='sidebar-button'>
                        <div className='svg-container'>
                            <Payments/>
                        </div>
                        <p className='sidebar-text'>Покупки и платные подписки</p>
                    </button>
                </div>
                <div className='dropdown-section'>
                    <button className='sidebar-button'>
                        <div className='svg-container'>
                            <UserData/>
                        </div>
                        <p className='sidebar-text'>Вашие данные на YouTube</p>
                    </button>
                </div>
                <div className='dropdown-section'>
                    <button className='sidebar-button'>
                        <div className='svg-container'>
                            <SettingsIcon/>
                        </div>
                        <p className='sidebar-text'>Настройки</p>
                    </button>
                </div>
                <div className='dropdown-section' style={{border: 'none'}}>
                    <button className='sidebar-button'>
                        <div className='svg-container'>
                            <HelpIcon/>
                        </div>
                        <p className='sidebar-text'>Справка</p>
                    </button>
                    <button className='sidebar-button'>
                        <div className='svg-container'>
                            <AlertIcon/>
                        </div>
                        <p className='sidebar-text'>Отправить отзыв</p>
                    </button>
                    <a href="http://localhost:4173/sign-in">Войти</a>
                </div>
            </div>
        </>
    )
}

export default UserMenu;