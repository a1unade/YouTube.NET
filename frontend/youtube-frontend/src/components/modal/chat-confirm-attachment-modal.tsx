import React, { useEffect } from 'react';

const ChatConfirmAttachmentModal = (props: {
  file: File;
  active: boolean;
  setActive: React.Dispatch<React.SetStateAction<boolean>>;
  setShouldSendFile: React.Dispatch<React.SetStateAction<boolean>>;
  setFile: React.Dispatch<React.SetStateAction<File | null>>;
  setHasAttachment: React.Dispatch<React.SetStateAction<boolean>>;
  setFileIsLoading: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const {
    file,
    active,
    setActive,
    setShouldSendFile,
    setFileIsLoading,
    setFile,
    setHasAttachment,
  } = props;

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

  const handleCancel = () => {
    setActive(false);
    setFile(null);
  };

  const handleConfirm = () => {
    setActive(false);
    setShouldSendFile(true);
    setHasAttachment(true);
    setFileIsLoading(true);
  };

  if (file === null) return null;

  return (
    <div>
      <div
        className={`modal-overlay ${active ? 'active' : ''}`}
        onClick={() => setActive(false)}
        role="dialog"
      >
        <div className="modal-content" style={{ width: 650 }} onClick={(e) => e.stopPropagation()}>
          <p>Вы действительно хотите добавить файл</p>
          <p>{file.name} ?</p>
          <div className="confirm-buttons">
            <button className="btn-red" onClick={handleCancel}>
              Отмена
            </button>
            <button className="btn-blue" onClick={handleConfirm}>
              Подтвердить
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ChatConfirmAttachmentModal;
