import React, { useEffect, useState } from 'react';
import apiClient from '../../utils/apiClient.ts';
import { UploadFileResponse } from '../../interfaces/file/upload-file-response.ts';
import { useAlerts } from '../../hooks/alert/use-alerts.tsx';

const ChatWindowInputSection = (props: {
  chatId: string | null;
  userId: string | null;
  sendMessage: (
    message: string,
    userId: string,
    fileId: string | null,
    chatId: string | null,
  ) => Promise<void>;
  setFile: React.Dispatch<React.SetStateAction<File | null>>;
  setConfirmIsActive: React.Dispatch<React.SetStateAction<boolean>>;
  shouldSendFile: boolean;
  file: File | null;
  setShouldSendFile: React.Dispatch<React.SetStateAction<boolean>>;
  setLoading: React.Dispatch<React.SetStateAction<boolean>>;
  setHasAttachment: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
  const {
    chatId,
    userId,
    sendMessage,
    setFile,
    setConfirmIsActive,
    shouldSendFile,
    setShouldSendFile,
    file,
    setLoading,
    setHasAttachment,
  } = props;

  const [messageText, setMessageText] = useState('');
  const [attachmentId, setAttachmentId] = useState<string | null>(null);
  const { addAlert } = useAlerts();

  useEffect(() => {
    const sendButton = document.getElementById('sendButton') as HTMLDivElement;

    if (messageText.trim() !== '' || file) {
      sendButton.style.display = 'flex';
    } else {
      sendButton.style.display = 'none';
    }
  }, [messageText, file]);

  useEffect(() => {
    if (shouldSendFile && file !== null) {
      const formData = new FormData();
      formData.append('UserId', userId!);
      formData.append('File', file);

      apiClient
        .post<UploadFileResponse>('file/UploadFile', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        })
        .then((response) => {
          if (response.data.isSuccessfully) {
            setAttachmentId(response.data.entityId);
          } else {
            addAlert(response.data.message!);
          }
        })
        .finally(() => {
          setLoading(false);
          setShouldSendFile(false);
        });
    }
  }, [shouldSendFile, file]);

  const handleSendButtonClick = () => {
    setMessageText('');
    sendMessage(messageText, userId!, attachmentId, chatId).then(() => setHasAttachment(false));
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFile = event.target.files![0];
    if (selectedFile) {
      setFile(selectedFile);
      setConfirmIsActive(true);
    }
  };

  return (
    <div className="input-section-layout">
      <div className="input-section-attachment">
        <svg
          fill="#D3D3D3"
          height="32px"
          width="32px"
          version="1.1"
          id="Capa_1"
          xmlns="http://www.w3.org/2000/svg"
          xmlnsXlink="http://www.w3.org/1999/xlink"
          viewBox="0 0 479.254 479.254"
          xmlSpace="preserve"
          stroke="#D3D3D3"
          onClick={() => document.getElementById('fileInput')!.click()}
        >
          <g id="SVGRepo_bgCarrier" strokeWidth="0" />
          <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round" />
          <g id="SVGRepo_iconCarrier">
            {' '}
            <path d="M451.471,117.602h-3.887c-17.036,0-30.851-13.821-30.851-30.858v-3.877c0-15.334-12.434-27.77-27.753-27.77H256.987 c-15.351,0-27.786,12.436-27.786,27.77v3.877c0,17.037-13.815,30.858-30.851,30.858H139.77v-3.119V87.705 c0-16.122-13.121-29.251-29.251-29.251c-16.131,0-29.251,13.129-29.251,29.251v127.091c0,16.123,13.12,29.252,29.251,29.252 c16.13,0,29.251-13.129,29.251-29.252v-51.441c0-4.461-3.608-8.069-8.069-8.069c-4.46,0-8.069,3.608-8.069,8.069v51.441 c0,7.235-5.879,13.113-13.112,13.113s-13.113-5.878-13.113-13.113V87.705c0-7.233,5.879-13.112,13.113-13.112 s13.112,5.879,13.112,13.112v26.777v3.119h-18.155v52.939v16.675l10.086-2.6v-16.658v-4.604c0-8.92,7.227-16.139,16.139-16.139 c15.209,0,16.139,13.396,16.139,16.139c0,3.242,0,12.971,0,12.971l66.949-17.21c1.159-0.299,2.334-0.457,3.507-0.457 c6.391,0,11.97,4.35,13.563,10.544l45.248,176.043c1.93,7.486-2.602,15.146-10.086,17.067L90.976,407.561 c-7.47,1.924-15.186-2.758-17.07-10.087L28.658,221.431c-1.93-7.484,2.602-15.145,10.087-17.068l34.453-8.856v-16.658v-61.246 H27.784C12.434,117.602,0,130.053,0,145.388v70.213l0.015-0.017v152.45c0,18.44,14.957,33.381,33.365,33.381h24.87l0.024,0.079 c3.436,13.332,15.436,22.663,29.197,22.663c2.528,0,5.059-0.331,7.525-0.962l84.743-21.78h157.28h35.618h73.254 c18.408,0,33.363-14.941,33.363-33.381V145.388C479.254,130.053,466.819,117.602,451.471,117.602z" />{' '}
          </g>
        </svg>
        <input id="fileInput" type="file" onChange={handleFileChange} style={{ display: 'none' }} />
      </div>
      <textarea
        id="messageInput"
        value={messageText}
        onChange={(e) => setMessageText(e.target.value)}
        className="input-section-textarea"
        rows={4}
        maxLength={100}
        placeholder="Сообщение..."
      />
      <div
        className="input-section-send-message-button"
        id="sendButton"
        onClick={handleSendButtonClick}
      >
        <div className="input-section-attachment">
          <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" stroke="#000000">
            <g id="SVGRepo_bgCarrier" strokeWidth="0" />
            <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round" />
            <g id="SVGRepo_iconCarrier">
              <path
                d="M12 5V19M12 5L6 11M12 5L18 11"
                stroke="lightgray"
                strokeWidth="1.2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </g>
          </svg>
        </div>
      </div>
    </div>
  );
};

export default ChatWindowInputSection;
