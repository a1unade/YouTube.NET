/* istanbul ignore file */
import { ChannelShortType } from '../../types/channel/channel-short-type.ts';
import ChannelElement from '../../components/channel';
import ChannelNavigation from './components/channel-navigation.tsx';

const ChannelFeatured = () => {
  const channel: ChannelShortType = {
    id: 1,
    name: 'ExileShow',
    image:
      'https://yt3.googleusercontent.com/mx5F0oIYca4pqKVSjL3ZdzsfYDONENYB46N5QJzUinDwr6oOVdF8GPpzeIojesWAmT3fU1nc=s160-c-k-c0x00ffffff-no-rj',
    subscribersCount: 5260000,
    description: 'Я не знаю, зачем создал этот канал, но он вроде как развлекательный',
    videoCount: 295,
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
          <ChannelNavigation />
        </div>
      </div>
    </>
  );
};

export default ChannelFeatured;
