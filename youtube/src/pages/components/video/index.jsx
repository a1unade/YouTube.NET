import React, {useEffect, useState} from "react";
import {useLocation} from 'react-router-dom';
import MainVideo from "./components/MainVideo";
import SearchVideo from "./components/SearchVideo";
import apiClient from "../../../utils/apiClient";

const Video = ({id}) => {
    const [channel, setChannel] = useState(null);
    const [video, setVideo] = useState(null);
    const location = useLocation();
    const currentPage = location.pathname;

    useEffect(() => {
        const fetchData = async () => {
            try {
                const videoResponse = await apiClient.get(`/videos?part=snippet&part=statistics&id=${id}`);
                setVideo(videoResponse.data.items[0]);

                const channelResponse = await apiClient.get(`/channels?part=snippet&part=brandingSettings&id=${videoResponse.data.items[0].snippet.channelId}`);
                console.log(channelResponse.data.items[0])
                setChannel(channelResponse.data.items[0]);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [id]);

    const renderVideoContent = () => {
        switch (true) {
            case currentPage === '/':
                return (
                    <>
                        <MainVideo video={video} channel={channel}/>
                    </>
                );
            case currentPage.includes('/search/'):
                return (
                    <>
                        <SearchVideo video={video} channel={channel}/>
                    </>
                );
            case currentPage.includes('/watch/'):
                return (
                    <>
                        <MainVideo video={video} channel={channel}/>
                    </>
                );
            default:
                return null;
        }
    }

    if (!channel || !video) {
        return ("");
    } else {
        return (
            <>
                {renderVideoContent()}
            </>
        );
    }
}

export default Video;