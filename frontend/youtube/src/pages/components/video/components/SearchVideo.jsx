import {useNavigate} from 'react-router-dom';
import {formatDate, formatViews} from "../../../../utils/formatFunctions";
import ylogo from "../../../../assets/img/ylogo.svg";

const SearchVideo = ({video, channel}) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="search-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${id}`)}>
                    <img src={video.snippet.thumbnails.medium.url}></img>
                </div>
                <div className="search-video-info" title={video.snippet.localized.title} style={{marginLeft: 30}}>
                    <div className="search-video-details">
                        <div className="search-video-name">
                            <span><b>{video.snippet.localized.title}</b></span>
                        </div>
                        <div className="info">
                            <ul>
                                <li>{formatViews(video.statistics.viewCount, 'views')}</li>
                                <li>{formatDate(video.snippet.publishedAt)}</li>
                            </ul>
                        </div>
                    </div>
                    <div className="search-results-author-info">
                        <div>
                            <div className="author-image"
                                 onClick={() => navigate(`/channel/${channel.snippet.customUrl}`)}>
                                <img src={channel.snippet.thumbnails.high.url} alt=''/>
                            </div>
                        </div>
                        <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center', gap: 10}}>
                            <span>{video.snippet.channelTitle}</span>
                            <div style={{width: 20, height: 20}}>
                                <img src={ylogo} alt=''/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default SearchVideo;