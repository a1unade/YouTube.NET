import React from "react";
import '../../assets/sign.css'
import Google_logo from "./components/Google_logo";

const Sign = () => {
    return(
        <>
            <div className="content">
                <div className="sign-container">
                    <div>
                        <Google_logo />
                        <h1>Вход</h1>
                        <span>Перейдите на YouTube</span>
                    </div>
                    <div className="input-container">
                        <input type="email" placeholder="Телефон или адрес эл. почты"></input>
                        <a href=""><b>Забыли адрес электронной почты?</b></a>
                    </div>
                    <div className="input-container">
                        <span>Работаете на чужом компьютере? Включите гостевой режим.</span>
                        <a href=""><b>Подробнее об использовании гостевого режима</b></a>
                    </div>
                    <div className="sign-buttons">
                        <button className="left-button">Создать аккаунт</button>
                        <button className="right-button">Далее</button>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Sign;