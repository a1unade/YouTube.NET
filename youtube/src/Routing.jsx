import { Routes, Route } from 'react-router-dom';
import Main from './pages/main_page';
import Player from './pages/player';
import Search from './pages/search';
import Featured from './pages/channel_page/Featured.jsx'
import Videos from './pages/channel_page/Videos.jsx'
import Playlists from './pages/channel_page/Playlists.jsx'
import Community from './pages/channel_page/Community.jsx'
import VideoGames from './pages/channel_page/VideoGames.jsx'
import ChannelPage from './pages/channel_page/ChannelPage.jsx';





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