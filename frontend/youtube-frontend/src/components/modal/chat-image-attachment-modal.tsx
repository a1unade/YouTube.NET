import React, { useEffect } from 'react';
import { useAlerts } from '../../hooks/alert/use-alerts.tsx';

const ChatAttachmentImageModal = (props: {
  src: string | undefined;
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { src, active, setActive } = props;
  const { addAlert } = useAlerts();
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

  const handleDownload = () => {
    const link = document.createElement('a');
    link.href = src!;
    link.setAttribute('download', 'filename');
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    addAlert('Файл скачан');
  };

  return (
    <div>
      <div
        className={`chat-modal-overlay ${active ? 'active' : ''}`}
        onClick={() => setActive(false)}
        role="dialog"
      >
        <div className="icon-container">
          <div onClick={handleDownload}>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              height="30"
              viewBox="0 0 24 24"
              width="30"
              focusable="false"
              aria-hidden="true"
            >
              <path d="M17 18v1H6v-1h11zm-.5-6.6-.7-.7-3.8 3.7V4h-1v10.4l-3.8-3.8-.7.7 5 5 5-4.9z" />
            </svg>
          </div>
          <div onClick={() => setActive(false)}>
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
        <div className="chat-modal-content" onClick={(e) => e.stopPropagation()}>
          <img src={src} alt="image" />
        </div>
      </div>
    </div>
  );
};
export default ChatAttachmentImageModal;
