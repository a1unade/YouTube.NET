import React, { useState } from 'react';
import apiClient from '../../utils/apiClient.ts';
import { useAlerts } from '../../hooks/alert/use-alerts.tsx';

const UploadPage = (props: { userId: string | null }) => {
  const { userId } = props;
  const { addAlert } = useAlerts();
  const [file, setFile] = useState<File | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFile = event.target.files?.[0];
    if (selectedFile) {
      setFile(selectedFile);
    }
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!file) return;

    setLoading(true);
    try {
      const metadataResponse = await apiClient.post<UploadMetadataResponse>('File/UploadMetadata', {
        fileName: file.name,
        size: file.size.toString(),
        contentType: file.type,
        userId: userId,
      });

      const metaStatus = metadataResponse.data.isSuccessfully;

      console.log('Sending metadata:', {
        fileName: file.name,
        size: file.size.toString(),
        contentType: file.type,
        userId: userId,
      });

      if (metaStatus) {
        const attachmentId = metadataResponse.data.entityId;

        const fileResponse = await apiClient.post<UploadFileResponse>(
          'File/UploadFile',
          {
            fileId: attachmentId,
            userId: userId,
            file: file,
          },
          {
            headers: { 'Content-Type': 'multipart/form-data' },
          },
        );

        if (fileResponse.data.isSuccessfully) {
          addAlert('Файл успешно загружен!');
        } else {
          addAlert(fileResponse.data.message!);
        }
      } else {
        addAlert(metadataResponse.data.message!);
      }
    } catch (error) {
      console.error('Error during uploads', error);
      addAlert('Произошла ошибка при загрузке.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="upload-page-container">
      <form className="upload-page-form" onSubmit={handleSubmit}>
        <input
          className="upload-page-file-input"
          type="file"
          onChange={handleFileChange}
          required
        />
        <button className="upload-page-submit-button" type="submit" disabled={loading}>
          {loading ? 'Загрузка...' : 'Отправить файл'}
        </button>
      </form>
    </div>
  );
};

export default UploadPage;

interface UploadMetadataResponse {
  isSuccessfully: boolean;
  entityId: string;
  message?: string;
}

interface UploadFileResponse {
  isSuccessfully: boolean;
  message?: string;
  entityId?: string;
}
