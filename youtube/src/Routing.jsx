import { Routes, Route } from 'react-router-dom';
import Main from './pages/main_page';
import Player from './pages/player';
import Search from './pages/search';

const Routing = () => {
    return (
        <Routes>
            <Route path='/' element={<Main />} />
            <Route path='/watch/:id' element={<Player />} />
            <Route path='/search/:request' element={<Search />} />
            <Route path="/channel/:customUrl" element={<ChannelPage />}></Route>
            <Route path="/channel/:customUrl/featured" element={<Featured />} />
            <Route path="/channel/:customUrl/videos"  element={<Videos />} />
            <Route path="/channel/:customUrl/playlists"  element={<Playlists />} />
            <Route path="/channel/:customUrl/community" element={<Community />} />
            <Route path="/channel/VideoGames" element={<VideoGames/>} />
        </Routes>
    );
}

export default Routing;