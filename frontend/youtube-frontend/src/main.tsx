/* istanbul ignore file */

import ReactDOM from 'react-dom/client';
import './index.css';
import LeftMenu from './components/layout/left-menu.tsx';
import Header from './components/layout/header.tsx';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Main from './pages/main';
import { useState, useEffect } from 'react';
import Player from './pages/player';
import Alert from './components/layout/alert.tsx';
import SaveVideoModal from './components/modal/save-video-modal.tsx';
import ShareModal from './components/modal/share-modal.tsx';
import ReportModal from './components/modal/report-modal.tsx';
import { useAlerts } from './hooks/alert/use-alerts.tsx';
import { AlertProvider } from './contexts/alert-provider.tsx';
import Subscriptions from './pages/subscriptions';
import Playlists from './pages/playlists';
import ChannelFeatured from './pages/channel';
import ChatModalWindow from './components/modal/chat-modal-window.tsx';
import { jwtDecode } from 'jwt-decode';
import { JWTTokenDecoded } from './interfaces/jwt-token/jwt-token-decoded.ts';
import AuthRedirect from './pages/auth';
import Premium from './pages/premium';
import UploadPage from './pages/upload';

export const App = () => {
  const [saveVideoActive, setSaveVideoActive] = useState(false);
  const [shareActive, setShareActive] = useState(false);
  const [reportVideoActive, setReportVideoActive] = useState(false);
  const [isOpen, setIsOpen] = useState<boolean>(() => {
    const savedMenuState = localStorage.getItem('menu');
    return savedMenuState !== null ? JSON.parse(savedMenuState) : true;
  });
  const [chatIsOpen, setChatIsOpen] = useState<boolean>(false);
  const [userId, setUserId] = useState<string | null>(null);

  useEffect(() => {
    const authCookie = document.cookie.split('; ').find((row) => row.startsWith('authCookie='));

    if (authCookie) {
      const token = authCookie.split('=')[1];
      try {
        const decodedToken = jwtDecode<JWTTokenDecoded>(token);

        if (decodedToken.exp * 1000 < Date.now()) {
          console.warn('Token has expired');
          document.cookie = 'authCookie=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
        } else {
          setUserId(decodedToken.Id);
        }
      } catch (error) {
        console.error('Invalid token', error);
      }
    } else {
      const userIdCookie = document.cookie.split('; ').find((row) => row.startsWith('userId='));

      if (userIdCookie) {
        const userIdValue = userIdCookie.split('=')[1];
        setUserId(userIdValue);
      }
    }
  }, []);

  useEffect(() => {
    const layoutPage = document.querySelector('.layout-page');
    if (layoutPage) {
      if (isOpen) {
        layoutPage.classList.remove('closed-menu');
      } else {
        layoutPage.classList.add('closed-menu');
      }
    }
    localStorage.setItem('menu', JSON.stringify(isOpen));
  }, [isOpen]);

  const handleMenuClick = () => {
    setIsOpen((prevIsOpen) => !prevIsOpen);
  };

  const { alerts, removeAlert } = useAlerts();

  return (
    <div>
      <div className="alert-container">
        {alerts.map((alert) => (
          <Alert key={alert.id} message={alert.message} onClose={() => removeAlert(alert.id)} />
        ))}
      </div>
      <div id="layout-page" className="layout-page">
        <Header onClick={handleMenuClick} userId={userId} />
        <LeftMenu isOpen={isOpen} setChatIsOpen={setChatIsOpen} chatIsOpen={chatIsOpen} />
        <div className="page-content">
          <Routes>
            <Route
              path="/"
              element={
                <Main
                  setSaveVideoActive={setSaveVideoActive}
                  setShareActive={setShareActive}
                  setReportVideoActive={setReportVideoActive}
                />
              }
            />
            <Route
              path="/watch/:id"
              element={
                <Player
                  setSaveActive={setSaveVideoActive}
                  setShareActive={setShareActive}
                  setReportVideoActive={setReportVideoActive}
                />
              }
            />
            <Route
              path="/channel/:id"
              element={
                <ChannelFeatured
                  setReportVideoActive={setReportVideoActive}
                  setShareActive={setShareActive}
                  setSaveVideoActive={setSaveVideoActive}
                />
              }
            />
            <Route
              path="/channel/:id/videos"
              element={
                <ChannelFeatured
                  setReportVideoActive={setReportVideoActive}
                  setShareActive={setShareActive}
                  setSaveVideoActive={setSaveVideoActive}
                />
              }
            />
            <Route
              path="/channel/:id/playlists"
              element={
                <ChannelFeatured
                  setReportVideoActive={setReportVideoActive}
                  setShareActive={setShareActive}
                  setSaveVideoActive={setSaveVideoActive}
                />
              }
            />
            <Route
              path="/channel/:id/community"
              element={
                <ChannelFeatured
                  setReportVideoActive={setReportVideoActive}
                  setShareActive={setShareActive}
                  setSaveVideoActive={setSaveVideoActive}
                />
              }
            />
            <Route
              path="/channel/:id/about"
              element={
                <ChannelFeatured
                  setReportVideoActive={setReportVideoActive}
                  setShareActive={setShareActive}
                  setSaveVideoActive={setSaveVideoActive}
                />
              }
            />
            <Route path="/feed/subscriptions" element={<Subscriptions />} />
            <Route path="/auth/:userId" element={<AuthRedirect />} />
            <Route path="/feed/playlists" element={<Playlists />} />
            <Route path="/premium" element={<Premium />} />
            <Route path="/upload" element={<UploadPage userId={userId} />} />
          </Routes>
        </div>
      </div>
      <SaveVideoModal active={saveVideoActive} setActive={setSaveVideoActive} />
      <ShareModal shareActive={shareActive} setShareActive={setShareActive} />
      <ReportModal active={reportVideoActive} setActive={setReportVideoActive} />
      <ChatModalWindow active={chatIsOpen} setActive={setChatIsOpen} userId={userId} />
    </div>
  );
};

ReactDOM.createRoot(document.getElementById('root')!).render(
  <AlertProvider>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </AlertProvider>,
);
