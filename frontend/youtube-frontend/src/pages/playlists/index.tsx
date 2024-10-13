import PlaylistItem from '../../components/playlist';

const Playlists = () => {
  return (
    <>
      <div
        style={{
          fontSize: 36,
          fontWeight: 'bold',
          textAlign: 'left',
        }}
      >
        <p>Плейлисты</p>
      </div>
      <div className="videos-list">
        {Array.from({ length: 6 }, (_, i) => (
          <div className="channel-video" key={i + 1}>
            <PlaylistItem id={`${i + 1}`} />
          </div>
        ))}
      </div>
    </>
  );
};

export default Playlists;
