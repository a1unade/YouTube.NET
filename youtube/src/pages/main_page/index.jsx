import React, { useEffect, useState } from "react";
import Video from "../components/video";
import apiClient from "../../utils/apiClient";

const Main = () => {
    const [videos, setVideos] = useState([])
    useEffect(() => {
        apiClient.get(`/videos?part=snippet%2Cstatistics%2Cplayer&chart=mostPopular&maxResults=8&maxHeight=300&maxWidth=350`)
            .then(data => {
                console.log(data.data);
                setVideos(data.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, []);

    if (videos.length === 0) {
        return ("");
    }
    else {
        return (
            <>
                <div className="content">
                    <div className="videos-list">
                        {videos.items.map(video => (<Video video={video} key={video.id} />))}
                    </div>
                </div>
            </>
        );
    }
}

export default Main