import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import apiClient from '../../utils/apiClient.js';
import Video from "../components/video/index.jsx";

const Search = () => {
    const {request} = useParams();
    const [results, setResults] = useState([]);

    useEffect(() => {
        apiClient.get(`/search?part=snippet&order=viewCount&q=${request}&type=video&videoDefinition=high&maxResults=5`)
            .then(response => {
                console.log(response.data);
                setResults(response.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }, [request]);
    if (results.length === 0) {
        return ('');
    } else {
        return (
            <>
                <div className="search-results">
                    <div className='videos-list'>
                        {results.items.map(video => (<Video id={video.id.videoId} key={video.etag}/>))}
                    </div>
                </div>
            </>
        );
    }
}

export default Search;