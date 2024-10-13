import { ChannelShortType } from '../../types/channel/channel-short-type.ts';
import ChannelElement from '../../components/channel';
import ChannelNavigation from './components/channel-navigation.tsx';
import React, { useEffect, useState } from 'react';
import ChannelVideos from './components/channel-videos.tsx';
import { useLocation } from 'react-router-dom';
import { navigationMap } from '../../types/channel/channel-navigation-map.ts';
import ChannelAbout from './components/channel-about.tsx';
import ChannelPlaylists from './components/channel-playlists.tsx';

const ChannelFeatured = (props: {
  setSaveVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
  setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
  setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { setSaveVideoActive, setShareActive, setReportVideoActive } = props;
  const location = useLocation();
  const [selected, setSelected] = useState<string>('Видео');

  useEffect(() => {
    const pathSegments = location.pathname.split('/');
    const lastSegment = pathSegments[pathSegments.length - 1];

    if (navigationMap[lastSegment]) {
      setSelected(navigationMap[lastSegment]);
    } else {
      setSelected('Видео');
    }
  }, [location.pathname]);

  const channel: ChannelShortType = {
    id: 1,
    name: 'ExileShow',
    image:
      'https://yt3.googleusercontent.com/mx5F0oIYca4pqKVSjL3ZdzsfYDONENYB46N5QJzUinDwr6oOVdF8GPpzeIojesWAmT3fU1nc=s160-c-k-c0x00ffffff-no-rj',
    subscribersCount: 5260000,
    description: 'Я не знаю, зачем создал этот канал, но он вроде как развлекательный',
    videoCount: 295,
  };

  const renderContent = () => {
    switch (selected) {
      case 'Видео':
        return (
          <ChannelVideos
            setSaveVideoActive={setSaveVideoActive}
            setShareActive={setShareActive}
            setReportVideoActive={setReportVideoActive}
          />
        );
      case 'Плейлисты':
        return <ChannelPlaylists />;
      case 'О канале':
        return <ChannelAbout description={channel.description} />;
    }
  };

  return (
    <>
      <div style={{ height: 15 }} />
      <div className="channel-section">
        <div className="channel-thumbnail">
          <img
            alt="thumbnail"
            src="https://yt3.googleusercontent.com/0E_3gmbELIY28NKzMExD0wSWcGXkj0C9Ds3xM9JJuTu33xZyBazLbqMofM41Ev4-H39hR_d6Qw=w1707-fcrop64=1,00005a57ffffa5a8-k-c0xffffffff-no-nd-rj"
          />
        </div>
        <div>
          <ChannelElement channel={channel} />
          <ChannelNavigation selected={selected} setSelected={setSelected} />
          {renderContent()}
        </div>
      </div>
    </>
  );
};

export default ChannelFeatured;
