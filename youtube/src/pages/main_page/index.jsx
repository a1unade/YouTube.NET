import {useEffect, useState} from 'react';
import Video from '../components/video/index.jsx';
import apiClient from '../../utils/apiClient';
import ad from '../../assets/img/cyberad.jpeg';
import {useSelector} from "react-redux";

const Main = () => {
    const [videos, setVideos] = useState([]);
    const {premium} = useSelector(state => state.user.premium);

    useEffect(() => {
        apiClient.get(`/videos?part=id&chart=mostPopular&regionCode=RU&maxResults=20`)
            .then(response => {
                setVideos(response.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, []);

    if (videos.length === 0) {
        return ('');
    } else {
        return (
            <>
                <div className='main-page'>
                    {premium ?
                        <div className="ad">
                            <div>
                                <img src={ad} alt="ad"/>
                                <p style={{fontSize: 12, margin: 0, marginTop: 10, textAlign: 'left'}}>* Чтобы отключить
                                    рекламу, купите подписку Premium</p>
                            </div>
                            <div style={{display: 'flex', flexDirection: 'column', justifyContent: 'space-between'}}>
                            <span>Вы играете за V, наёмника в поисках уникального устройства, позволяющего обрести бессмертие.
                                Вы сможете менять киберимпланты, навыки и стиль игры своего персонажа, исследуя огромный открытый мир, где ваши поступки влияют на ход сюжета и всё, что вас окружает.</span>
                                <b>
                                    <p>В комплект издания входит:</p>
                                    <i style={{textAlign: "left"}}>
                                        <ul>
                                            <li>Игра Cyberpunk 2077;</li>
                                            <li>Масштабное обновление 2.0, полностью переработавшим различные аспекты
                                                игры;
                                            </li>
                                            <li>Код на загрузку дополнения Phantom Liberty («Призрачная свобода»).</li>
                                        </ul>
                                    </i>
                                </b>
                            </div>
                        </div>
                        :
                        null
                    }
                    <h3 style={{marginLeft: 0}}>Новые</h3>
                    <div className='content'>
                        <div className='videos-list'>
                            {videos.items.map(video => (<Video id={video.id} key={video.etag}/>))}
                        </div>
                    </div>
                </div>
            </>
        );
    }
}

export default Main