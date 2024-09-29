import {useEffect, useState} from "react";
import apiClient from "../../../utils/apiClient.js";

const Sport = () => {
    const [isFollow, setIsFollow] = useState(false);
    const [owners, setOwner] = useState([])


    const titles = ['подписчик', 'подписчика', 'подписчиков'];
    const cases = [2, 0, 1, 1, 1, 2];
    function formatFollowersCount(count) {
        if (count < 1000 && count >= 0) {
            const index = count % 100 > 4 && count % 100 < 20 ? 2 : cases[Math.min(count % 10, 5)];
            return `${count} ${titles[index]}`;

        } else if (count >= 1000 && count < 1000000) {

            let formattedCount = (count / 1000).toFixed(1); // Округляем до одного десятичного знака
            if (formattedCount.endsWith('0')) {
                formattedCount = formattedCount.slice(0, -2); // Удаляем последний символ
            } else {
                formattedCount = formattedCount.replace('.', ',');
            }
            return `${formattedCount} тыс. ${titles[1]}`;

        } else if (count >= 1000000 && count <= 999999999) {

            let formattedCount = (count / 1000000).toFixed(2);
            if (formattedCount.endsWith('00')) {
                formattedCount = formattedCount.slice(0, -3);
            } else if (formattedCount.endsWith('0')) {
                formattedCount = formattedCount.slice(0, -1).replace('.', ',');
            } else {
                formattedCount = formattedCount.replace('.', ',');
            }
            return `${formattedCount} млн. ${titles[2]}`;
        } else {
            // Для чисел больше 999 999 999 (миллиардов) или меньше 0
            return 'Invalid count';
        }
    }



    useEffect(() => {
        apiClient.get(`/channels?part=snippet&part=statistics&part=brandingSettings&part=contentDetails&id=${"UCEgdi0XIXXZ-qJOFPf4JSKw"}&maxResults=5`)
            .then(response => {
                console.log(response.data.items[0])
                setOwner(response.data.items[0]);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, []);

    if(!owners){
        return (
            <div></div>
        )
    } else {
        return(
            <>
                <div className="sport-header">
                    <div className="sport-description">
                        <div className="main-image">
                            <img id="img" draggable="false" className="style-scope yt-img-shadow" alt="" width="80"
                                 src="//yt3.googleusercontent.com/mUhuJiCiL8jf0Ngf9sh7BFBZCO0MUL2JyH_5ElHbV2fd13hxZ9zQ3-x-YePA_-PCUUH360G0=s176-c-k-c0x00ffffff-no-rj-mo"/>
                        </div>
                        <div className="sport-main-description">
                            <h2>Спорт</h2>
                            <span>{formatFollowersCount(owners.statistics.subscriberCount)}</span>
                        </div>
                        <div className='subscription'>
                            {isFollow
                                ? <button id='True' onClick={() => setIsFollow(false)}>Подписаться</button>
                                : <button id='False' onClick={() => setIsFollow(true)}>
                                    Вы подписаны
                                </button>
                            }
                        </div>
                    </div>
                </div>
                <div className="sport-main-content">
                    <div className="sport-full-playlist">
                    <span>
                        Лучшие моменты
                    </span>
                        <button>
                            Посмотреть все
                        </button>
                    </div>
                </div>

                <div className="main-content videogames" style={{marginLeft: 60}}>
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
            </>
        );
    }

}
export default Sport;