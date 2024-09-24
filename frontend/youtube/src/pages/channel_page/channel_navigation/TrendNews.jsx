import InTrend from "./InTrend.jsx";

const TrendNews = () => {
    return(
        <>
            <InTrend></InTrend>

            {/*тут будут видосы*/}
            <div className="main-content videogames">
                <div className="recomendation">


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
            {/*    */}

        </>
    );
};
export default TrendNews;