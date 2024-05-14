import {useNavigate} from "react-router-dom";
import {formatDate, formatViews} from "../../../../utils/formatFunctions.js";

// eslint-disable-next-line react/prop-types
const MyChannelVideo = ({video}) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="main-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${video.id}`)}>
                    <img src={video.previewImg} alt='img'></img>
                </div>
                <div className="main-video-info" title={video.name}>
                    <div className="main-video-details">
                        <div className="main-video-name">
                            <span><b>{video.name}</b></span>
                        </div>
                        <div className="info">
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

export default MyChannelVideo;