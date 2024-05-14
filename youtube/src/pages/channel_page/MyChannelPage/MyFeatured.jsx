import MyChannelPage from "./MyChannelPage.jsx";

const MyFeatured = () => {

    return (
        <>
            <MyChannelPage />
            <div className="main-content">
                <div className="for-you">
                    <span>
                        Для вас
                    </span>
                </div>

                <div className="video-list" >
                    <div className="main-video">
                        <div className="preview" id="preview-HbWkGxnOEXw">
                            <img src="https://i.ytimg.com/vi/HbWkGxnOEXw/mqdefault.jpg"/></div>
                        <div className="main-video-info"
                             title="НОВЫЙ AMONG US в РЕАЛЬНОЙ ЖИЗНИ! Масленников, Егорик, Дилара, Гаврилина, Tenderlybae, Адушкина">
                            <div>
                                <div className="author-image"><img
                                    src="https://yt3.ggpht.com/ytc/AIdro_lHZSIl6H18odBSY-tuY2TQ5iWF79EBcGnBv3AKKfiCGqE=s800-c-k-c0x00ffffff-no-rj"
                                    alt=""/>
                                </div>
                            </div>
                            <div className="main-video-details">
                                <div className="main-video-name"><span><b>НОВЫЙ AMONG US в РЕАЛЬНОЙ ЖИЗНИ! Масленников, Егорик, Дилара, Гаврилина, Tenderlybae, Адушкина</b></span>
                                </div>
                                <div className="info"><span>Дима Масленников</span>
                                    <ul>
                                        <li>3,4 млн просмотров</li>
                                        <li>1 день назад</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <hr className="separator" style={{marginTop: 100}}/>

                <div className="video-button">
                    <span>
                        Видео
                    </span>
                    <div className="reproduce">
                        <button>
                            <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                                 focusable="false">
                                <path d="m7 4 12 8-12 8V4z"></path>
                            </svg>
                            Воспроизвести все
                        </button>

                    </div>

                </div>

                <div className="video">ТУТ ВИДОСЫ</div>
                <hr className="separator" style={{marginTop: 20}}/>

                <div className="video-list">
                </div>
            </div>
        </>
    );
};
export default MyFeatured;