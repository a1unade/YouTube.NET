import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import apiClient from '../../utils/apiClient';

const Player = () => {
    const [video, setVideo] = useState(null);
    const { id } = useParams();
    // useEffect(() => {
    //     apiClient.get(`/videos?part=snippet%2Cstatistics&id=${id}`)
    //         .then(response => {
    //             console.log(response.data);
    //             setVideo(response.data);
    //         })
    //         .catch(error => {
    //             console.error('Error fetching data:', error);
    //         });
    // }, []);
    // console.log(video)
    return (
        <>
            <div className='content'>
                <iframe width='1080' height='720' src={`https://www.youtube.com/embed/${id}?autoplay=1`} frameBorder={0} allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share' allowFullScreen='true'></iframe>
            </div>
        </>
    );
}

export default Player;