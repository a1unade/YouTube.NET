import { PlaylistResponse } from '../../interfaces/playlist/playlist-response.ts';

const PlaylistItem = (props: { id: string }) => {
  const { id } = props;

  const playlist: PlaylistResponse = {
    id: id,
    title: 'Плейлист',
    videoCount: parseInt(id) + Math.floor(Math.random() * (6 - 2 + 1) + 2),
    thumbnail:
      'https://i.ytimg.com/vi/7GO1OZB0UMY/hq720.jpg?sqp=-oaymwEcCNAFEJQDSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLDnsFkzFMULYWN-WY6Bg3TVlIwSGQ',
  };

  return (
    <div className="playlist-item">
      <div className="playlist-background-item" />
      <div className="playlist-item-thumbnail">
        <img src={playlist.thumbnail} alt="" />
        <div className="playlist-item-video-counter">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            height="24"
            viewBox="0 0 24 24"
            width="24"
            focusable="false"
            aria-hidden="true"
          >
            <path d="M22 7H2v1h20V7zm-9 5H2v-1h11v1zm0 4H2v-1h11v1zm2 3v-8l7 4-7 4z" />
          </svg>
          <span>{playlist.videoCount} видео</span>
        </div>
        <div className="playlist-item-hover-element">
          <svg
            width="24"
            height="24"
            viewBox="0 0 16 16"
            xmlns="http://www.w3.org/2000/svg"
            fill="#fff"
          >
            <path d="m11.596 8.697-6.363 3.692c-.54.313-1.233-.066-1.233-.697V4.308c0-.63.692-1.01 1.233-.696l6.363 3.692a.802.802 0 0 1 0 1.393z" />
          </svg>
          <span>ВОСПРОИЗВЕСТИ ВСЕ</span>
        </div>
      </div>
      <div className="playlist-item-title">
        <span>{playlist.title}</span>
      </div>
      <button>Посмотреть весь плейлист</button>
    </div>
  );
};

export default PlaylistItem;
