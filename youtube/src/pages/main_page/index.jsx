import {useEffect, useState} from 'react';
import Video from '../components/video/index.jsx';
import apiClient from '../../utils/apiClient';

const Main = () => {
    const [videos, setVideos] = useState([]);

    useEffect(() => {
        apiClient.get(`/videos?part=id&chart=mostPopular&regionCode=RU&maxResults=20`)
            .then(response => {
                setVideos(response.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, []);

    if (videos.length === 0) {
        return ('');
    } else {
        return (
            <>
                <div className='main-page'>
                    <h3 style={{marginLeft: 0}}>Новые</h3>
                    <div className='content'>
                        <div className='videos-list'>
                            {videos.items.map(video => (<Video id={video.id} key={video.etag}/>))}
                        </div>
                    </div>
                </div>
            </>
        );
    }
}

export default Main