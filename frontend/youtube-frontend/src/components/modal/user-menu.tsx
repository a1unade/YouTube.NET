import React, { useEffect, useRef } from 'react';
import {
  AlertIcon,
  Exit,
  HelpIcon,
  Payments,
  SettingsIcon,
  StudioMenu,
  UserData,
} from '../../assets/icons.tsx';

const UserMenu = (props: {
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
  buttonRef: React.RefObject<HTMLButtonElement>;
}) => {
  const { active, setActive, buttonRef } = props;
  const modalRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      const target = event.target as Node;

      if (
        modalRef.current &&
        !modalRef.current.contains(target) &&
        buttonRef.current &&
        !buttonRef.current.contains(target)
      ) {
        setActive(false);
      }
    };

    if (active) {
      document.addEventListener('mousedown', handleClickOutside);
    } else {
      document.removeEventListener('mousedown', handleClickOutside);
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [active, buttonRef, setActive]);

  return (
    <>
      {active && (
        <div ref={modalRef} style={{ width: 300 }} className="actions-modal-window">
          <div className="user-menu">
            <div className="user-menu-image">
              <img alt="avatar" src="https://avatars.githubusercontent.com/u/113981832?v=4" />
            </div>
            <div className="user-menu-info">
              <p>
                <b>name</b>
              </p>
              <span>@username</span>
              <a style={{ marginTop: 10 }} href="">
                Посмотреть канал
              </a>
            </div>
          </div>
          <div className="dropdown-section">
            <button className="sidebar-button" style={{ gap: 0 }}>
              <div className="svg-container">
                <Exit />
              </div>
              <p className="sidebar-text">Выйти</p>
            </button>
          </div>
          <div className="dropdown-section">
            <button className="sidebar-button" style={{ gap: 0 }}>
              <div className="svg-container">
                <StudioMenu />
              </div>
              <p className="sidebar-text">Творческая студия YouTube</p>
            </button>
            <button className="sidebar-button" style={{ gap: 0 }}>
              <div className="svg-container">
                <Payments />
              </div>
              <p className="sidebar-text">Покупки и платные подписки</p>
            </button>
          </div>
          <div className="dropdown-section">
            <button className="sidebar-button" style={{ gap: 0 }}>
              <div className="svg-container">
                <UserData />
              </div>
              <p className="sidebar-text">Ваши данные на YouTube</p>
            </button>
          </div>
          <div className="dropdown-section">
            <button className="sidebar-button" style={{ gap: 0 }}>
              <div className="svg-container">
                <SettingsIcon />
              </div>
              <p className="sidebar-text">Настройки</p>
            </button>
          </div>
          <div className="dropdown-section" style={{ border: 'none' }}>
            <button className="sidebar-button" style={{ gap: 0 }}>
              <div className="svg-container">
                <HelpIcon />
              </div>
              <p className="sidebar-text">Справка</p>
            </button>
            <button className="sidebar-button" style={{ gap: 0 }}>
              <div className="svg-container">
                <AlertIcon />
              </div>
              <p className="sidebar-text">Отправить отзыв</p>
            </button>
          </div>
        </div>
      )}
    </>
  );
};

export default UserMenu;
