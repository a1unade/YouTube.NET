

const VideoGames = () => {

    return (
        <>
            <div className="video-games" style={{marginLeft: 20}}>
                <div className="channel-description" style={{marginLeft: 20}}>

                    <div className='main-image' style={{marginBottom: 0}}>
                        <img id="img" draggable="false" className="style-scope yt-img-shadow" alt=""
                             style={{width: 72, height: 72}}
                             src="//yt3.googleusercontent.com/pzvUHajbQDLDt63gKFYUX445k3VprUs8CeJFpNTxGQZlk0grOSkAqU8Th1_C97dyYM3nENgjbw=s120-c-k-c0x00ffffff-no-rj"/>
                    </div>
                    <div className='main-description' style={{marginBottom: 0}}>
                        <h1 style={{marginTop: 32}}>Игры</h1>
                    </div>

                </div>
                <div className="main-content videogames">
                    <div className="recomendation">
                        <div className="recomendation-span">
                            <span>Рекомендовано</span>
                        </div>

                        {/*это затычка тут переделать */}
                        <div id='main-page' className='main-page'>
                            <div className='content'>
                                <div className='videos-list'>
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
                            </div>
                        </div>
                        {/*     */}

                    </div>




                </div>


            </div>

        </>
    );
};
export default VideoGames;