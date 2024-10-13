import PlaylistItem from '../../../components/playlist';

const ChannelPlaylists = () => {
  return (
    <>
      <div style={{ height: 15 }} />
      <div className="videos-list">
        {Array.from({ length: 2 }, (_, i) => (
          <div className="channel-video" key={i + 1}>
            <PlaylistItem id={`${i + 1}`} />
          </div>
        ))}
      </div>
    </>
  );
};

export default ChannelPlaylists;
