import {Routes, Route} from 'react-router-dom';
import Main from './pages/main_page';
import Player from './pages/player';
import Search from './pages/search';
import Featured from './pages/channel_page/Featured.jsx'
import Videos from './pages/channel_page/Videos.jsx'
import Playlists from './pages/channel_page/Playlists.jsx'
import Community from './pages/channel_page/Community.jsx'
import VideoGames from './pages/channel_page/channel_navigation/VideoGames.jsx'
import ChannelPage from './pages/channel_page/ChannelPage.jsx';
import Music from "./pages/channel_page/channel_navigation/Music.jsx";
import Films from "./pages/channel_page/channel_navigation/Films.jsx";
import FilmsCatalog from "./pages/channel_page/channel_navigation/FilmsCatalog.jsx";
import FilmsPurchases from "./pages/channel_page/channel_navigation/FilmsPurchases.jsx";
import Sport from "./pages/channel_page/channel_navigation/Sport.jsx";
import TrendMusic from "./pages/channel_page/channel_navigation/TrendMusic.jsx";
import TrendFilm from "./pages/channel_page/channel_navigation/TrendFilm.jsx";
import TrendVideoGames from "./pages/channel_page/channel_navigation/TrendVideoGames.jsx";
import TrendNews from "./pages/channel_page/channel_navigation/TrendNews.jsx";
import MusicCommunity from "./pages/channel_page/channel_navigation/MusicCommunity.jsx";
import MusicFeatured from "./pages/channel_page/channel_navigation/MusicFeatured.jsx";
import ContentAdd from "./pages/admin_panel/ContentAdd.jsx";
import AdminAddVideo from "./pages/admin_panel/AdminAddVideo.jsx";
import AdminAddPlaylist from "./pages/admin_panel/AdminAddPlaylist.jsx";
import Branding from "./pages/admin_panel/Brending.jsx";
import MainDetails from "./pages/admin_panel/MainDetails.jsx";
import AccountSettings from "./pages/settings/AccountSettings.jsx";
import PaymentsSettings from "./pages/settings/PaymentsSettings.jsx";

const Routing = () => {
    return (
        <Routes>
            <Route path='/' element={<Main/>}/>
            <Route path='/watch/:id' element={<Player/>}/>
            <Route path='/search/:request' element={<Search/>}/>
            <Route path="/channel/:customUrl" element={<ChannelPage/>}></Route>
            <Route path="/channel/:customUrl/featured" element={<Featured/>}/>
            <Route path="/channel/:customUrl/videos" element={<Videos/>}/>
            <Route path="/channel/:customUrl/playlists" element={<Playlists/>}/>
            <Route path="/channel/:customUrl/community" element={<Community/>}/>
            <Route path="/channel/VideoGames" element={<VideoGames/>}/>
            <Route path="/channel/Music" element={<Music/>}/>
            <Route path="/channel/Sport" element={<Sport/>}/>
            <Route path="/feed" element={<Films/>}/>
            <Route path="/feed/Catalog" element={<FilmsCatalog/>}/>
            <Route path="/feed/Purchases" element={<FilmsPurchases/>}/>
            <Route path="/feed/trending/news" element={<TrendNews/>}/>
            <Route path="/feed/trending/music" element={<TrendMusic/>}/>
            <Route path="/feed/trending/films" element={<TrendFilm/>}/>
            <Route path="/feed/trending/videogames" element={<TrendVideoGames/>}/>
            <Route path="/channel/Music/community" element={<MusicCommunity/>}/>
            <Route path="/channel/Music/featured" element={<MusicFeatured/>}/>
            <Route path="/channel/edit/id" element={<ContentAdd/>}/>
            <Route path="/channel/edit/addvideo" element={<AdminAddVideo/>}/>
            <Route path="/channel/edit/channelId/playlist" element={<AdminAddPlaylist/>}/>
            <Route path="/channel/edit/images" element={<Branding/>}/>
            <Route path="/channel/edit/details" element={<MainDetails/>}/>
            <Route path={"/settings/account"} element={<AccountSettings/>}/>
            <Route path={"/settings/payments"} element={<PaymentsSettings/>}/>
        </Routes>
    );
}

export default Routing;