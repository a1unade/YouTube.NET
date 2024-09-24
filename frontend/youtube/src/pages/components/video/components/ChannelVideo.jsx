import {useNavigate} from "react-router-dom";
import {formatDate, formatViews} from "../../../../utils/formatFunctions.js";

const ChannelVideo = ({video}) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="main-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${video.id}`)}>
                    <img src={video.snippet.thumbnails.medium.url} alt=''></img>
                </div>
                <div className="main-video-info" title={video.snippet.localized.title}>
                    <div className="main-video-details">
                        <div className="main-video-name">
                            <span><b>{video.snippet.localized.title}</b></span>
                        </div>
                        <div className="info">
                            <ul>
                                <li>{formatViews(video.statistics.viewCount, 'views')}</li>
                                <li>{formatDate(video.snippet.publishedAt)}</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default ChannelVideo;