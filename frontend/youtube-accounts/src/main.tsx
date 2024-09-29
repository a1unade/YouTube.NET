import { createRoot } from 'react-dom/client';

import './index.css';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import Register from './pages/register';
import Error from './pages/error';
import Layout from './components/layout.tsx';
import Sign from './pages/sign-in';

createRoot(document.getElementById('root')!).render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<Navigate to="/signin" />} />
      <Route path="/signup" element={<Layout element={<Register />} />} />
      <Route path="/error" element={<Layout element={<Error />} />} />
      <Route path="/signin" element={<Layout element={<Sign />} />} />
      <Route path="*" element={<span style={{ color: 'black' }}>404</span>} />
    </Routes>
  </BrowserRouter>,
);
