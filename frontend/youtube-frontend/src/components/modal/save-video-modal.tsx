import React, { useEffect, useState } from 'react';
import { PlaylistType } from '../../types/playlist/playlist-type.ts';
import { useAlerts } from '../../hooks/alert/use-alerts.tsx';

const SaveVideoModal = (props: {
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { active, setActive } = props;
  const { addAlert } = useAlerts();
  const [savedPlaylists, setSavedPlaylists] = useState<number[]>([]);

  const playlists: PlaylistType[] = [
    {
      id: 0,
      name: 'Смотреть позже',
      isPrivate: true,
    },
    {
      id: 1,
      name: 'Пример плейлиста',
      isPrivate: false,
    },
    {
      id: 2,
      name: 'Пример плейлиста с длинным названием',
      isPrivate: true,
    },
  ];

  useEffect(() => {
    if (active) {
      document.body.style.overflow = 'hidden';
    } else {
      document.body.style.overflow = '';
    }

    return () => {
      document.body.style.overflow = '';
    };
  }, [active]);

  const handlePlaylist = (id: number, name: string) => {
    if (savedPlaylists.includes(id)) {
      setSavedPlaylists((prevPlaylists) => prevPlaylists.filter((videoId) => videoId !== id));
      addAlert(`Видео удалено из плейлиста "${name}".`);
    } else {
      setSavedPlaylists((prevPlaylists) => [...prevPlaylists, id]);
      addAlert(`Видео добавлено в плейлист "${name}".`);
    }
  };

  return (
    <div>
      <div
        className={`modal-overlay ${active ? 'active' : ''}`}
        onClick={() => setActive(false)}
        role="dialog"
      >
        <div className="modal-content" onClick={(e) => e.stopPropagation()}>
          <div className="video-modal-window">
            <div className="save-video-modal-playlists">
              <div
                style={{
                  display: 'flex',
                  flexDirection: 'row',
                  justifyContent: 'space-between',
                  width: '100%',
                  alignItems: 'center',
                  marginBottom: 10,
                }}
              >
                <span style={{ fontSize: 16 }}>Выберите плейлист</span>
                <div className="close-modal-button" onClick={() => setActive(false)}>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    enableBackground="new 0 0 24 24"
                    height="24"
                    viewBox="0 0 24 24"
                    width="24"
                    focusable="false"
                    aria-hidden="true"
                  >
                    <path d="m12.71 12 8.15 8.15-.71.71L12 12.71l-8.15 8.15-.71-.71L11.29 12 3.15 3.85l.71-.71L12 11.29l8.15-8.15.71.71L12.71 12z" />
                  </svg>
                </div>
              </div>
              {playlists.map((playlist: PlaylistType) => (
                <div
                  title={playlist.name}
                  className="save-video-modal-single-playlist"
                  key={playlist.id}
                  onClick={() => handlePlaylist(playlist.id, playlist.name)}
                >
                  <input
                    type="checkbox"
                    id={`checkbox-${playlist.name.toString()}`}
                    name={`checkbox-${playlist.name.toString()}`}
                    defaultChecked={savedPlaylists.includes(playlist.id)}
                  />
                  <span>{playlist.name}</span>
                  {playlist.isPrivate ? (
                    <div style={{ marginLeft: 'auto' }}>
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        height="18"
                        viewBox="0 0 18 18"
                        width="18"
                        focusable="false"
                        aria-hidden="true"
                      >
                        <path d="M13 5c0-2.21-1.79-4-4-4S5 2.79 5 5v1H3v11h12V6h-2V5zM6 5c0-1.65 1.35-3 3-3s3 1.35 3 3v1H6V5zm8 2v9H4V7h10zm-7 4c0-1.1.9-2 2-2s2 .9 2 2-.9 2-2 2-2-.9-2-2z" />
                      </svg>
                    </div>
                  ) : null}
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default SaveVideoModal;
