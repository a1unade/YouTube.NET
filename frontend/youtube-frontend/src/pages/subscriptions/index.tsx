import { ChannelShortType } from '../../types/channel/channel-short-type.ts';
import ChannelElement from '../../components/channel';

const Subscriptions = () => {
  const channels: ChannelShortType[] = [
    {
      id: 1,
      name: 'Tech Insights',
      image: 'https://via.placeholder.com/150/0000FF/808080?text=Tech+Insights',
      subscribersCount: 850000,
      description: 'Latest technology reviews, tutorials, and gadget overviews.',
      videoCount: 320,
    },
    {
      id: 2,
      name: 'Fitness World',
      image: 'https://via.placeholder.com/150/FF0000/FFFFFF?text=Fitness+World',
      subscribersCount: 1200000,
      description: 'Your daily dose of fitness routines, health tips, and nutrition.',
      videoCount: 540,
    },
    {
      id: 3,
      name: 'Cooking Paradise',
      image: 'https://via.placeholder.com/150/FFFF00/000000?text=Cooking+Paradise',
      subscribersCount: 670000,
      description: 'Delicious recipes and cooking techniques from around the world.',
      videoCount: 200,
    },
    {
      id: 4,
      name: 'Travel Adventures',
      image: 'https://via.placeholder.com/150/008000/FFFFFF?text=Travel+Adventures',
      subscribersCount: 950000,
      description: 'Join us on amazing travel adventures and cultural experiences.',
      videoCount: 410,
    },
    {
      id: 5,
      name: 'Science Simplified',
      image: 'https://via.placeholder.com/150/FFA500/000000?text=Science+Simplified',
      subscribersCount: 540000,
      description: 'Simplifying complex scientific concepts and theories for everyone.',
      videoCount: 180,
    },
  ];

  return (
    <div>
      <div className="channel-short-elements-list">
        <div
          style={{
            fontSize: 36,
            fontWeight: 'bold',
            textAlign: 'left',
            width: 900,
          }}
        >
          <p>Каналы, на которые вы подписаны</p>
        </div>
        {channels.map((channel: ChannelShortType) => (
          <ChannelElement channel={channel} key={channel.id} />
        ))}
      </div>
    </div>
  );
};

export default Subscriptions;
