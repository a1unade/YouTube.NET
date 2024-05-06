import ContentAdd from "./ContentAdd.jsx";
import VideoInfo from "./VideoInfo.jsx";
import {useState} from "react";
import axios from "axios";

const AdminAddVideo = () => {

    const [selectedFile, setSelectedFile] = useState(null);
    const [uploading, setUploading] = useState(false);
    const [uploadSuccess, setUploadSuccess] = useState(false);
    const handleFileChange = (event) => {
        setSelectedFile(event.target.files[0]);
    };
    const video = {
        img: "https://i.ytimg.com/vi/HbWkGxnOEXw/mqdefault.jpg",
        like: 23,
        dislike: 22,
        comment: 3,
        date: "2020-02-20",
        view: 3232,
        name : "Прятки Cs:go"
    }

    const handleSubmit = async (event) => {
        event.preventDefault();
        setUploading(true);

        const formData = new FormData();
        formData.append('video', selectedFile);

        try {
            const response = await axios.post('http://localhost:5041/api/Channel/upload', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            console.log('Response:', response);
            setUploadSuccess(true);
        } catch (error) {
            console.error('Ошибка при отправке запроса:', error);
        } finally {
            setUploading(false);
        }
    };
    return (
        <>
            <ContentAdd></ContentAdd>
            <div className="table-header">
                <div className="table-header-titles">
                    <span>
                        Дата
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        Просмотры
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        Комментарии
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        Нравится
                    </span>
                </div>
            </div>
            <hr className="separator" style={{marginTop: 10}}/>
            {!video ? <div className="all-video">
                <div className="no-image">
                    <img alt="" className="style-scope ytcp-video-section-content"
                         src="https://www.gstatic.com/youtube/img/creator/no_content_illustration_upload_video_v3.svg"/>
                </div>
                <div className="no-content-title">
                    <span>
                        Здесь пока ничего нет.
                    </span>

                </div>
                <div className="add-video-button">

                    <button>
                        ДОБАВИТЬ ВИДЕО
                    </button>
                </div>

            </div> : <VideoInfo video={video}/>}

            {uploadSuccess ? (
                <div className="upload-success-message">
                    Видео успешно загружено!
                </div>
            ) : null}

            <div className="add-video-button">
                <form onSubmit={handleSubmit}>
                    <input type="file" name="video" accept="video/*" onChange={handleFileChange} />
                    <button type="submit" disabled={uploading}>
                        {uploading ? 'Загрузка...' : 'ДОБАВИТЬ ВИДЕО'}
                    </button>
                </form>
            </div>
        </>
    );
};

export default AdminAddVideo;