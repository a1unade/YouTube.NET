import ChannelSettings from "./ChannelSettings.jsx";
import ReferencesEdit from "./ReferencesEdit.jsx";
import {useState} from "react";

const MainDetails = () => {
    const [components, setComponents] = useState([]);

    const handleEditClick = () => {
        setComponents([...components, <ReferencesEdit key={components.length} />]);
    };
    function updateCharacterCount(e) {
        if(e.target.value.length === 0){
            charCountElement.textContent = `0/1000`
        }
        const maxLength = 0;
        const currentLength = e.target.value.length;
        const remainingCharacters = maxLength + currentLength;
        const charCountElement = document.getElementById("charCount");
        if (charCountElement) {
            charCountElement.textContent = `${remainingCharacters}/1000`;
        }
    }


    return(
        <>

            <ChannelSettings/>
            <div className="channel-settings-header">
                <div className="channel-settings-page">
                    <div className="channel-settings-info">
                        <h2>
                            Название канала
                        </h2>
                    </div>
                    <div className="channel-settings-info">
                    <span>
                        Придумайте название канала, которое будет представлять вас и ваш контент.
                        Если вы укажете другое название или поменяете фото профиля, эти изменения будут видны только на YouTube,
                        а не во всех сервисах Google.
                    </span>
                        <br/>
                        <input type="text" placeholder="Укажите название канала"/>
                    </div>
                </div>

                <div className="channel-settings-page">
                    <div className="channel-settings-info">
                        <h2>
                            Псевдоним
                        </h2>
                    </div>
                    <div className="channel-settings-info">
                    <span>
                        Придумайте уникальное имя пользователя из букв и цифр. Вернуть прежний псевдоним можно в течение 14 дней.
                        Псевдонимы можно менять два раза каждые 14 дней.
                    </span>
                        <br/>
                        <input type="text" placeholder="Введите псевдоним"/>
                    </div>
                </div>

                <div className="channel-settings-page">
                    <div className="channel-settings-info">
                        <h2>
                            Описание канала
                        </h2>
                    </div>
                    <div className="channel-settings-info" style={{marginBottom: 70}}>
                    <span>
                        Придумайте уникальное имя пользователя из букв и цифр. Вернуть прежний псевдоним можно в течение 14 дней.
                        Псевдонимы можно менять два раза каждые 14 дней.
                    </span>
                        <br/>
                        <textarea  className="description-input" maxLength={1000}
                                  onInput={(e) => updateCharacterCount(e)}
                                  placeholder="Расскажите аудитории о своем канале. Описание будет показываться в разных разделах YouTube, в том числе на вкладке &quot;О канале&quot; и результатах поиска."/>
                        <p id="charCount" className="char-count"></p>
                    </div>
                </div>

                <div className="channel-settings-page" style={{marginTop: 70,}}>
                    <div className="channel-settings-info">
                        <h2>
                            URL канала
                        </h2>
                    </div>
                    <div className="channel-settings-info">
                    <span>
                        Это стандартный веб-адрес вашего канала. Набор цифр и букв в конце ссылки – уникальный идентификатор канала.
                    </span>
                        <input className="readonly" value="https://www.youtube.com/channel/UCSuy7FHRwEve8epmZrpEfwg" readOnly/>
                        {/*<svg viewBox="0 0 24 24" preserveAspectRatio="xMidYMid meet" focusable="false"*/}
                        {/*     className="style-scope tp-yt-iron-icon">*/}
                        {/*    <g className="style-scope tp-yt-iron-icon">*/}
                        {/*        <path d="M19,6v15H8V6H19 M15,2H4v16h1V3h10V2L15,2z M20,5H7v17h13V5L20,5z"*/}
                        {/*              className="style-scope tp-yt-iron-icon"></path>*/}
                        {/*    </g>*/}
                        {/*</svg>*/}

                    </div>
                </div>
                <div className="channel-settings-page" style={{marginBottom:0}}>
                    <div className="channel-settings-info">
                        <h2>
                            Ссылки
                        </h2>
                    </div>
                    <div className="channel-settings-info">
                    <span>
                        {/* eslint-disable-next-line react/no-unescaped-entities */}
                        Поделитесь внешними ссылками с аудиторией. Они будут видны в профиле канала и на вкладке "О канале".
                    </span>
                        <br/>



                    </div>
                </div>
                <div className="">
                    <div className="edit-references" onClick={handleEditClick}>
                        <svg viewBox="0 0 24 24" preserveAspectRatio="xMidYMid meet" focusable="false"
                             className="style-scope tp-yt-iron-icon">
                            <g className="style-scope tp-yt-iron-icon">
                                <path d="M20,12h-8v8h-1v-8H3v-1h8V3h1v8h8V12z"
                                      className="style-scope tp-yt-iron-icon"></path>
                            </g>
                        </svg>
                        <span>
                                ДОБАВИТЬ ССЫЛКУ
                            </span>

                    </div>
                    {components.map((Component, index) => (
                        <div className="references-container" key={index}>{Component}</div>
                    ))}
                </div>

                <div className="channel-settings-page">
                    <div className="channel-settings-info">
                        <h2>
                            Контактная информация
                        </h2>
                    </div>
                    <div className="channel-settings-info">
                    <span>
                        {/* eslint-disable-next-line react/no-unescaped-entities */}
                        Укажите, как связаться с вами по вопросам сотрудничества. Зрители могут увидеть адрес электронной почты на вкладке "О канале".
                    </span>
                        <div className="email-input">
                            <label>Электронная почта</label>
                            <input type="email" placeholder="Адрес электронной почты"/>
                        </div>
                    </div>
                </div>

            </div>
        </>
    );
};

export default MainDetails;