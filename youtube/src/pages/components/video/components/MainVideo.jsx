import React from "react";
import { useNavigate } from 'react-router-dom';
import { formatDate, formatViews } from "../../../../utils/formatFunctions";

const MainVideo = ({ video, channel }) => {
    const navigate = useNavigate();
    return (
        <>
            <div className="main-video">
                <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${video.id}`)}>
                    <img src={video.snippet.thumbnails.medium.url}></img>
                </div>
                <div className="main-video-info" title={video.snippet.localized.title}>
                    <div style={{ marginRight: 20 }}>
                        <div className="author-image" onClick={() => navigate(`/channel/${channel.snippet.customUrl}`)}>
                            <img src={channel.snippet.thumbnails.high.url} alt='' />
                        </div>
                    </div>
                    <div className="main-video-details">
                        <div className="main-video-name">
                            <span><b>{video.snippet.localized.title}</b></span>
                        </div>
                        <div className="info">
                            <span>{video.snippet.channelTitle}</span>
                            <ul>
                                <li>{formatViews(video.statistics.viewCount)}</li>
                                <li>{formatDate(video.snippet.publishedAt)}</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default MainVideo;