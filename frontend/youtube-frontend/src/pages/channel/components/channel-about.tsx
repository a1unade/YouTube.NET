import ChannelLink from './channel-link.tsx';
import { formatViews } from '../../../utils/format-functions.ts';
import { ChannelStats } from '../../../types/channel/channel-stats.ts';

const ChannelAbout = (props: { description: string }) => {
  const { description } = props;
  const links: string[] = [
    't.me/exilemusic13',
    'vk.com/exilemusic',
    'instagram.com/exile_music',
    'steamcommunity.com/id/exile_music',
  ];

  const statistics: ChannelStats = {
    subscribers: 5280000,
    videos: 296,
    views: 850485931,
    country: 'Россия',
  };

  return (
    <div>
      <div>
        <h3>О канале</h3>
        <span style={{ fontSize: 15, marginLeft: 20 }}>{description}</span>
      </div>
      <div>
        <h3>Ссылки</h3>
        <div className="channel-link-list">
          {links.map((link) => (
            <ChannelLink link={link} />
          ))}
        </div>
      </div>
      <div>
        <h3>Статистика</h3>
        <div className="channel-link-list">
          <div className="channel-link">
            <div className="channel-link-icon" style={{ width: 25, height: 25 }}>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                height="24"
                viewBox="0 0 24 24"
                width="24"
                focusable="false"
                aria-hidden="true"
              >
                <path d="M11.72 11.93C13.58 11.59 15 9.96 15 8c0-2.21-1.79-4-4-4S7 5.79 7 8c0 1.96 1.42 3.59 3.28 3.93C4.77 12.21 2 15.76 2 20h18c0-4.24-2.77-7.79-8.28-8.07zM8 8c0-1.65 1.35-3 3-3s3 1.35 3 3-1.35 3-3 3-3-1.35-3-3zm3 4.9c5.33 0 7.56 2.99 7.94 6.1H3.06c.38-3.11 2.61-6.1 7.94-6.1zm5.68-1.46-.48-.88C17.31 9.95 18 8.77 18 7.5s-.69-2.45-1.81-3.06l.49-.88C18.11 4.36 19 5.87 19 7.5c0 1.64-.89 3.14-2.32 3.94zm2.07 1.69-.5-.87c1.7-.98 2.75-2.8 2.75-4.76s-1.05-3.78-2.75-4.76l.5-.87C20.75 3.03 22 5.19 22 7.5s-1.24 4.47-3.25 5.63z" />
              </svg>
            </div>
            <span style={{ fontSize: 14 }}>{formatViews(statistics.subscribers, 'followers')}</span>
          </div>
          <div className="channel-link">
            <div className="channel-link-icon" style={{ width: 25, height: 25 }}>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                enableBackground="new 0 0 24 24"
                height="24"
                viewBox="0 0 24 24"
                width="24"
                focusable="false"
                aria-hidden="true"
              >
                <path d="m10 8 6 4-6 4V8zm11-5v18H3V3h18zm-1 1H4v16h16V4z" />
              </svg>
            </div>
            <span style={{ fontSize: 14 }}>{statistics.videos} видео</span>
          </div>
          <div className="channel-link">
            <div className="channel-link-icon" style={{ width: 25, height: 25 }}>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                enableBackground="new 0 0 24 24"
                height="24"
                viewBox="0 0 24 24"
                width="24"
                focusable="false"
                aria-hidden="true"
              >
                <path d="M22 6v7h-1V7.6l-8.5 7.6-4-4-5.6 5.6-.7-.7 6.4-6.4 4 4L20.2 7H15V6h7z" />
              </svg>
            </div>
            <span style={{ fontSize: 14 }}>{formatViews(statistics.views, 'views')}</span>
          </div>
          <div className="channel-link">
            <div className="channel-link-icon" style={{ width: 25, height: 25 }}>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                height="24"
                viewBox="0 0 24 24"
                width="24"
                focusable="false"
                aria-hidden="true"
              >
                <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zM3 12c0-.7.09-1.37.24-2.02L8 14.71v.79c0 1.76 1.31 3.22 3 3.46v1.98c-4.49-.5-8-4.32-8-8.94zm8.5 6C10.12 18 9 16.88 9 15.5v-1.21l-5.43-5.4C4.84 5.46 8.13 3 12 3c1.05 0 2.06.19 3 .53V5c0 .55-.45 1-1 1h-3v2c0 .55-.45 1-1 1H8v3h6c.55 0 1 .45 1 1v4h2c.55 0 1 .45 1 1v.69C16.41 20.12 14.31 21 12 21v-3h-.5zm7.47-.31C18.82 16.73 18 16 17 16h-1v-3c0-1.1-.9-2-2-2H9v-1h1c1.1 0 2-.9 2-2V7h2c1.1 0 2-.9 2-2V3.95c2.96 1.48 5 4.53 5 8.05 0 2.16-.76 4.14-2.03 5.69z" />
              </svg>
            </div>
            <span style={{ fontSize: 14 }}>{statistics.country}</span>
          </div>
        </div>
      </div>
      <div style={{ height: 200 }} />
    </div>
  );
};

export default ChannelAbout;
