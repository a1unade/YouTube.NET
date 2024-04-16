import React from "react";
import {useNavigate} from 'react-router-dom';
import {formatDate, formatViews} from "../../../../utils/formatFunctions";

const SearchVideo = ({video, channel}) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="search-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${id}`)}>
                    <img src={video.snippet.thumbnails.medium.url}></img>
                </div>
                <div className="search-video-info" title={video.snippet.localized.title}>
                    <div style={{marginRight: 20}}>
                        <div className="author-image" onClick={() => navigate(`/channel/${channel.snippet.customUrl}`)}>
                            <img src={channel.snippet.thumbnails.high.url} alt=''/>
                        </div>
                    </div>
                    <div className="search-video-details">
                        <div className="search-video-name">
                            <span><b>{video.snippet.localized.title}</b></span>
                        </div>
                        <div className="info">
                            <span>{video.snippet.channelTitle}</span>
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

export default SearchVideo;