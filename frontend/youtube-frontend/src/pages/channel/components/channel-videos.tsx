import Video from '../../../components/video';
import React from 'react';
import Filters from '../../../components/layout/filters.tsx';

const ChannelVideos = (props: {
  setSaveVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
  setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
  setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { setSaveVideoActive, setShareActive, setReportVideoActive } = props;

  const filters = [
    { id: 0, name: 'Новые' },
    { id: 1, name: 'Популярные' },
    { id: 2, name: 'Старые' },
  ];

  return (
    <>
      <div style={{ height: 15 }} />
      <Filters filters={filters} />
      <div className="videos-list">
        {Array.from({ length: 24 }, (_, i) => (
          <div className="channel-video" key={i + 1}>
            <Video
              id={`${i + 1}`}
              setSaveVideoActive={setSaveVideoActive}
              setReportVideoActive={setReportVideoActive}
              setShareActive={setShareActive}
            />
          </div>
        ))}
      </div>
    </>
  );
};

export default ChannelVideos;
