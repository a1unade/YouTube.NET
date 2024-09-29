import {useNavigate} from 'react-router-dom';
import {formatDate, formatViews} from "../../../../utils/formatFunctions";
import ylogo from '../../../../assets/img/ylogo.svg';

// eslint-disable-next-line react/prop-types
const MainVideo = ({video, channel}) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="main-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${video.id}`)}>
                    <img src={video.snippet.thumbnails.medium.url}></img>
                </div>
                <div className="main-video-info" title={video.snippet.localized.title}>
                    <div style={{marginRight: 20}}>
                        <div className="author-image" onClick={() => navigate(`/channel/${channel.snippet.customUrl}`)}>
                            <img src={channel.snippet.thumbnails.default.url} alt=''/>
                        </div>
                    </div>
                    <div className="main-video-details">
                        <div className="main-video-name">
                            <span><b>{video.snippet.localized.title}</b></span>
                        </div>
                        <div className="info">
                            <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center', gap: 10}}>
                                <span>{video.snippet.channelTitle}</span>
                                <div style={{width: 20, height: 20}}>
                                    <img src={ylogo} alt=''/>
                                </div>
                            </div>
                            <ul>
                                <li>{formatViews(video.statistics.viewCount, 'views')}</li>
                                <li>{formatDate(video.snippet.publishedAt)}</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
        ;
}

export default MainVideo;