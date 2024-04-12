import { Routes, Route } from 'react-router-dom';
import Main from './pages/main_page';
import Player from './pages/player';
import Search from './pages/search';

const Routing = () => {
    return (
        <Routes>
            <Route path='/' element={<Main />} />
            <Route path='/watch/:id' element={<Player />} />
            <Route path='/channel/:customUrl' element={'channel'} />
            <Route path='/search/:request' element={<Search />} />
        </Routes>
    );
}

export default Routing;