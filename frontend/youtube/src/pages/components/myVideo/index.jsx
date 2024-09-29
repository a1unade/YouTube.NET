import {useEffect, useState} from "react";
import {useLocation} from 'react-router-dom';
import backendClient from "../../../utils/backendClient.js";
import MyMainVideo from "./components/MyMainVideo";
import MySearchVideo from "./components/MySearchVideo";

const MyVideo = ({id}) => {
    const [channel, setChannel] = useState(null);
    const [video, setVideo] = useState(null);
    const location = useLocation();
    const currentPage = location.pathname;

    useEffect(() => {
        const fetchData = async () => {
            try {
                const videoResponse = await backendClient.get(`/videos?part=snippet&part=statistics&id=${id}`);
                setVideo(videoResponse.data.items[0]);

                const channelResponse = await backendClient.get(`/channels?part=snippet&part=brandingSettings&id=${videoResponse.data.items[0].snippet.channelId}`);
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
                        <MyMainVideo video={video} channel={channel}/>
                    </>
                );
            case currentPage.includes('/search/'):
                return (
                    <>
                        <MySearchVideo video={video} channel={channel}/>
                    </>
                );
            case currentPage.includes('/watch/'):
                return (
                    <>
                        <MyMainVideo video={video} channel={channel}/>
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

export default MyVideo;