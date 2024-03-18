import { BrowserRouter, Routes, Route } from 'react-router-dom';
import './App.css';
import Sign from './pages/sign/index.jsx'
import Register from './pages/registration/index.jsx'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/signin' element={<Sign />}></Route>
        <Route path='/signup' element={<Register />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
