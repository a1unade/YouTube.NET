import React, { useEffect, useState } from 'react';
import Video from '../components/video';
import apiClient from '../../utils/apiClient';
import Header from '../components/header/index.jsx';
import LeftMenu from '../components/left_menu/index.jsx';

const Main = () => {
    const [videos, setVideos] = useState([]);
    const [isOpen, setOpen] = useState(true);

    useEffect(() => {
        apiClient.get(`/videos?part=snippet%2Cstatistics%2Cplayer&chart=mostPopular&regionCode=US&maxResults=20&maxHeight=300&maxWidth=350`)
            .then(response => {
                setVideos(response.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, []);

    const toggleMenu = () => {
        if (isOpen) {
            document.getElementById('main-page').style.marginLeft = '60px';
            var videos = document.getElementsByClassName('video');
            for (var i = 0; i < videos.length; i++) {
                videos[i].style.width = '360px';
                videos[i].style.height = '320px';
            }
        } else {
            document.getElementById('main-page').style.marginLeft = '220px';
            var videos = document.getElementsByClassName('video');
            for (var i = 0; i < videos.length; i++) {
                videos[i].style.width = '320px';
                videos[i].style.height = '300px';
            }
        }
        setOpen(!isOpen);
    }

    if (videos.length === 0) {
        return ('');
    }
    else {
        return (
            <>
                <div>
                    <LeftMenu isOpen={isOpen} />
                    <Header toggleMenu={toggleMenu} />
                </div>
                <div id='main-page' className='main-page'>
                    <h3>Новые</h3>
                    <div className='content'>
                        <div className='videos-list'>
                            {videos.items.map(video => (<Video video={video} key={video.id} />))}
                        </div>
                    </div>
                </div>
            </>
        );
    }
}

export default Main