import { formatViews } from '../../../utils/format-functions.ts';
import {
  ButtonDislikeIcon,
  ButtonDislikeIconFilled,
  ButtonLikeIcon,
  ButtonLikeIconFilled,
  DownloadIcon,
  ShareIcon,
} from '../../../assets/icons.tsx';
import React, { useRef, useState } from 'react';
import ActionsModal from '../../../components/modal/actions-modal.tsx';
import PremiumModal from '../../../components/modal/premium-modal.tsx';
import { useAlerts } from '../../../hooks/alert/use-alerts.tsx';

const VideoActions = (props: {
  setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
  setSaveActive: React.Dispatch<React.SetStateAction<boolean>>;
  setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { setShareActive, setReportVideoActive, setSaveActive } = props;
  const buttonRef = useRef<HTMLButtonElement>(null);
  const [subscribed, setSubscribed] = useState(false);
  const [liked, setLiked] = useState(false);
  const [disliked, setDisliked] = useState(false);
  const premium = false;
  const { addAlert } = useAlerts();
  const [actionsModal, setActionsModal] = useState(false);
  const [premiumModal, setPremiumModal] = useState(false);

  const channel = {
    snippet: {
      customUrl: 'example-channel',
      thumbnails: {
        default: {
          url: 'https://yt3.ggpht.com/tDMXnQ33Oz-56RsgOMMmCcF5YieuKTWLa2R8cwkofUq_kTtnF3-curv8Jw0xpwCXnjIqtXvXXg=s88-c-k-c0x00ffffff-no-rj',
        },
      },
    },
  };

  const toggleModal = () => {
    setActionsModal(!actionsModal);
  };

  const handleDownload = () => {
    if (!premium) {
      setPremiumModal(true);
    }
    // TODO: скачивание видео
  };

  const handleSubscribe = () => {
    setSubscribed(!subscribed);

    if (subscribed) {
      addAlert('Подписка отменена.');
    } else {
      addAlert('Подписка оформлена.');
    }
  };

  return (
    <div className="video-actions-list">
      <div className="player-channel-info">
        <div className="main-video-info">
          <div style={{ marginRight: 10 }}>
            <div className="author-image">
              <img src={channel.snippet.thumbnails.default.url} alt="" />
            </div>
          </div>
          <div className="main-video-details">
            <div className="main-video-name">
              <span style={{ fontSize: 16, fontWeight: 'bold' }}>Название канала</span>
            </div>
            <div className="info">
              <span style={{ fontSize: 12 }}>{formatViews(143675438, 'followers')}</span>
            </div>
          </div>
        </div>
        <button
          className={`subscribe-button ${subscribed ? 'subscribed' : ''}`}
          onClick={handleSubscribe}
        >
          {!subscribed ? 'Подписаться' : 'Вы подписаны'}
        </button>
      </div>
      <div className="video-actions-list">
        <div className="like-dislike-actions">
          <button
            onClick={() => {
              disliked ? setDisliked(false) : null;
              setLiked(!liked);
            }}
            style={{
              borderRight: '1px solid rgba(0, 0, 0, 0.1)',
              paddingLeft: 10,
            }}
            id="like-button"
          >
            {liked ? <ButtonLikeIconFilled /> : <ButtonLikeIcon />}
            <span>{formatViews(13646124, 'likes')}</span>
          </button>

          <button
            onClick={() => {
              liked ? setLiked(false) : null;
              setDisliked(!disliked);
            }}
            style={{ paddingRight: 10 }}
            id="dislike-button"
          >
            {disliked ? <ButtonDislikeIconFilled /> : <ButtonDislikeIcon />}
          </button>
        </div>
        <div className="like-dislike-actions">
          <button style={{ paddingRight: 20 }} onClick={() => setShareActive(true)}>
            <ShareIcon />
            <span>Поделиться</span>
          </button>
        </div>
        <div className="like-dislike-actions">
          <button style={{ paddingRight: 20 }} onClick={handleDownload}>
            <DownloadIcon />
            <span>Скачать</span>
          </button>
        </div>
        <div style={{ position: 'relative' }}>
          <div
            style={{
              borderRadius: '50%',
              width: 36,
              height: 36,
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
            }}
            className="like-dislike-actions"
          >
            <div>
              <button onClick={toggleModal} ref={buttonRef}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  height="24"
                  viewBox="0 0 24 24"
                  width="24"
                  focusable="false"
                  aria-hidden="true"
                >
                  <path d="M7.5 12c0 .83-.67 1.5-1.5 1.5s-1.5-.67-1.5-1.5.67-1.5 1.5-1.5 1.5.67 1.5 1.5zm4.5-1.5c-.83 0-1.5.67-1.5 1.5s.67 1.5 1.5 1.5 1.5-.67 1.5-1.5-.67-1.5-1.5-1.5zm6 0c-.83 0-1.5.67-1.5 1.5s.67 1.5 1.5 1.5 1.5-.67 1.5-1.5-.67-1.5-1.5-1.5z" />
                </svg>
              </button>
              <ActionsModal
                active={actionsModal}
                setActive={setActionsModal}
                setReportVideoActive={setReportVideoActive}
                setSaveActive={setSaveActive}
                buttonRef={buttonRef}
              />
            </div>
          </div>
        </div>
      </div>
      <PremiumModal active={premiumModal} setActive={setPremiumModal} />
    </div>
  );
};

export default VideoActions;
