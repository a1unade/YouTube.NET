import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { useAlerts } from '../../hooks/alert/use-alerts.tsx';
import { ChannelShortType } from '../../types/channel/channel-short-type.ts';
import { formatViews } from '../../utils/format-functions.ts';

/**
 * Карточка канала.
 *
 * Используется на страницах: "Подписки", "Канал".
 * @param {ChannelShortType} props.channel Данные о канале
 * @returns {JSX.Element}
 *
 * <ChannelElement channel={channelExample} />
 */

const ChannelElement = (props: { channel: ChannelShortType }): JSX.Element => {
  const { channel } = props;
  const navigate = useNavigate();
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
    <>
      <div className="channel-short-element-layout">
        <div
          className="channel-short-element-info"
          onClick={() => navigate(`/channel/${channel.id}`)}
        >
          <div className="channel-short-element-author-img">
            <img src={channel.image} alt={channel.name} />
          </div>
          <div className="channel-details">
            <p>{channel.name}</p>
            <ul>
              <li>@username</li>
              <li>{formatViews(channel.subscribersCount, 'followers')}</li>
              <li>{channel.videoCount} видео</li>
            </ul>
            <span>{channel.description}</span>
          </div>
        </div>
        <button
          className={`subscribe-button ${subscribed ? 'subscribed' : ''}`}
          onClick={handleSubscribe}
        >
          {!subscribed ? 'Подписаться' : 'Вы подписаны'}
        </button>
      </div>
    </>
  );
};

export default ChannelElement;
