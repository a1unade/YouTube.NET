import {useNavigate} from 'react-router-dom';
import {formatDate, formatViews} from "../../../../utils/formatFunctions";

const MySearchVideo = ({video, channel}) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="search-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${id}`)}>
                    <img src={video.previewImg} alt=''></img>
                </div>
                <div className="search-video-info" title={video.name} style={{marginLeft: 30}}>
                    <div className="search-video-details">
                        <div className="search-video-name">
                            <span><b>{video.name}</b></span>
                        </div>
                        <div className="info">
                            <ul>
                                <li>{formatViews(video.viewCount, 'views')}</li>
                                <li>{formatDate(video.releaseDate)}</li>
                            </ul>
                        </div>
                    </div>
                    <div className="search-results-author-info">
                        <div>
                            <div className="author-image"
                                 onClick={() => navigate(`/channel/${channel.name}`)}>
                                <img src={channel.mainImg} alt=''/>
                            </div>
                        </div>
                        <span>{video.name}</span>
                    </div>
                </div>
            </div>
        </>
    );
}

export default MySearchVideo;