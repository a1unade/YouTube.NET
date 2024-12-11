import React from 'react';

const ChatAddedAttachment = (props: {
  file: File | null;
  setFile: React.Dispatch<React.SetStateAction<File | null>>;
  isLoading: boolean;
  setHasAttachment: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const { file, setFile, isLoading, setHasAttachment } = props;

  return (
    <div className="input-section-layout chat-added-attachment-layout">
      {isLoading ? (
        <div className="loading-indicator">
          {/* Можно добавить SVG или CSS анимацию здесь, например, спиннер */}
          <span>Загрузка...</span>
        </div>
      ) : (
        <span>{file?.name}</span>
      )}
      <button
        onClick={() => {
          setFile(null);
          setHasAttachment(false);
        }}
        className="remove-attachment"
      >
        Удалить
      </button>
    </div>
  );
};

export default ChatAddedAttachment;
