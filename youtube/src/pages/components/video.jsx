import React, { useEffect, useState } from "react";
import '../../assets/video.css';
import { useNavigate } from 'react-router-dom'
import { formatDate, formatViews } from "../../utils/formatFunctions";
import apiClient from "../../utils/apiClient";

const Video = ({ video }) => {
    const navigate = useNavigate();
    const [channel, setChannel] = useState(null);

    useEffect(() => {
        apiClient.get(`/channels?part=snippet&id=${video.snippet.channelId}`)
            .then(response => {
                setChannel(response.data.items[0]);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, []);

    if (!channel) {
        return ("");
    }
    else {
        console.log(channel);
        return (
            <>
                <div className="video">
                    <div className="preview" id={`preview-${video.id}`} onClick={() => navigate(`/watch/${video.id}`)}>
                        <img src={video.snippet.thumbnails.medium.url}></img>
                    </div>
                    {/* player

                    <div className="hidden" id={`player-${video.id}`} onClick={() => navigate(`/channel/${channel.snippet.customUrl}`)}>
                        <iframe width="350" height="197" src={`https://www.youtube.com/embed/${video.id}?autoplay=1&mute=1&controls=0&modestbranding=1`} frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen=""></iframe>
                    </div> */
                    }
                    <div className="video-info" title={video.snippet.localized.title}>
                        <div style={{ marginRight: 20 }}>
                            <div className="author-image" onClick={() => navigate(`/channel/${channel.snippet.customUrl}`)}>
                                <img src={channel.snippet.thumbnails.high.url} alt='' />
                            </div>
                        </div>
                        <div className="video-details">
                            <div className="video-name">
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
}

export default Video;