import {useNavigate} from 'react-router-dom';
import {formatDate, formatViews} from "../../../../utils/formatFunctions";

const MyMainVideo = ({video, channel}) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="main-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${video.id}`)}>
                    <img src={video.previewImage}></img>
                </div>
                <div className="main-video-info" title={video.name}>
                    <div style={{marginRight: 20}}>
                        <div className="author-image" onClick={() => navigate(`/channel/@${channel.Name}`)}>
                            <img src={channel.MainImg} alt=''/>
                        </div>
                    </div>
                    <div className="main-video-details">
                        <div className="main-video-name">
                            <span><b>{video.name}</b></span>
                        </div>
                        <div className="info">
                            <span>{video.snippet.channelTitle}</span>
                            <ul>
                                <li>{formatViews(video.viewCount, 'views')}</li>
                                <li>{formatDate(video.releaseDate)}</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default MyMainVideo;