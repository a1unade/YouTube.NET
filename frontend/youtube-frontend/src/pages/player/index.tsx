// noinspection HttpUrlsUsage

import ReactPlayer from 'react-player';
import VideoActions from './components/video-actions.tsx';
import Description from './components/description.tsx';
import React from 'react';
import VideoRecommendations from './components/video-recommendations.tsx';
import CommentSection from './components/comment-section.tsx';

const Player = (props: {
  setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
  setSaveActive: React.Dispatch<React.SetStateAction<boolean>>;
  setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { setShareActive, setSaveActive, setReportVideoActive } = props;

  const handleContextMenu = (event: React.MouseEvent) => {
    event.preventDefault();
  };

  // noinspection SpellCheckingInspection
  return (
    <>
      <div style={{ height: 15 }} />
      <div className="player-page-layout">
        <div className="player-section">
          <div>
            <div className="player-container">
              <ReactPlayer
                className="player"
                url="http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4"
                controls={true}
                config={{
                  file: {
                    attributes: {
                      controlsList: 'nodownload',
                      disablePictureInPicture: true,
                    },
                  },
                }}
                onContextMenu={handleContextMenu}
              />
            </div>
            <div className="player-video-title">
              <p>Название видео</p>
            </div>
            <VideoActions
              setReportVideoActive={setReportVideoActive}
              setSaveActive={setSaveActive}
              setShareActive={setShareActive}
            />
          </div>
          <Description />
          <CommentSection />
        </div>
        <VideoRecommendations
          setShareActive={setShareActive}
          setReportVideoActive={setReportVideoActive}
          setSaveActive={setSaveActive}
        />
      </div>
    </>
  );
};

export default Player;
