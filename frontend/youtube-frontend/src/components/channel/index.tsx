import { ChannelShortType } from '../../types/channel/channel-short-type.ts';
import { useState } from 'react';
import { useAlerts } from '../../hooks/alert/use-alerts.tsx';
import { formatViews } from '../../utils/format-functions.ts';

const ChannelElement = (props: { channel: ChannelShortType }) => {
  const { channel } = props;
  const [subscribed, setSubscribed] = useState(true);
  const { addAlert } = useAlerts();

  const handleSubscribe = () => {
    setSubscribed(!subscribed);

    if (subscribed) {
      addAlert('Подписка отменена.');
    } else {
      addAlert('Подписка оформлена.');
    }
  };

  return (
    <div className="channel-short-element-layout">
      <div className="channel-short-element-author-img">
        <img src={channel.image} alt={channel.name} />
      </div>
      <div className="channel-details">
        <p>{channel.name}</p>
        <ul>
          <li>@username</li>
          <li>{formatViews(channel.subscribersCount, 'followers')}</li>
        </ul>
        <p>{channel.description}</p>
      </div>
      <button
        className={`subscribe-button ${subscribed ? 'subscribed' : ''}`}
        onClick={handleSubscribe}
      >
        {!subscribed ? 'Подписаться' : 'Вы подписаны'}
      </button>
    </div>
  );
};

export default ChannelElement;