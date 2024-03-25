import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Main from './pages/main_page';
import Player from './pages/player';

const Routing = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element={<Main />} />
                <Route path='/watch/:id' element={<Player />} />
                <Route path='/channel/:customUrl' element={'channel'} />
            </Routes>
        </BrowserRouter>
    );
}

export default Routing;