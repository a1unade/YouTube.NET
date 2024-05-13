import {BrowserRouter, Routes, Route, Navigate} from 'react-router-dom';
import './App.css';
import Sign from './pages/sign/index.jsx'
import Register from './pages/registration/index.jsx'
import Error from "./pages/error/index.jsx";
import ChangeAvatar from "./pages/change/index.jsx";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Navigate to="/signin" />} />
                <Route path='/signin' element={<Sign/>}></Route>
                <Route path='/signup' element={<Register/>}></Route>
                <Route path='/error' element={<Error/>}></Route>
                <Route path='/change/:id' element={<ChangeAvatar/>}></Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;
