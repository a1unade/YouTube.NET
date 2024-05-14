import ContentAdd from "./ContentAdd.jsx";
import VideoInfo from "./VideoInfo.jsx";
import {useState} from "react";
import axios from "axios";

const AdminAddVideo = () => {

    const [videoData, setVideoData] = useState({
        selectedVideo: null, // Выбранное видео
        selectedImage: "", // Выбранное изображение
        videoName: "", // Название видео
        videoDescription: "", // Описание видео
        userId : "685749e0-eadb-4c7b-9af0-ec53a54d223d"
    });
    const [uploading, setUploading] = useState(false);
    const [uploadSuccess, setUploadSuccess] = useState(false);
    const handleFileChange = (event) => {
        const fieldName = event.target.name;
        const file = event.target.files[0];
        setVideoData(prevState => ({
            ...prevState,
            [fieldName]: file
        }));
    };

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setVideoData(prevState => ({
            ...prevState,
            [name]: value
        }));
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

        try {
            // Создаем объект FormData для отправки данных на сервер
            const formData = new FormData();
            formData.append('Video', videoData.selectedVideo); // Добавляем видео
            formData.append('ImgUrl', videoData.selectedImage); // Добавляем изображение
            formData.append('Name', videoData.videoName); // Добавляем название видео
            formData.append('Description', videoData.videoDescription); // Добавляем описание видео
            formData.append('UserId', videoData.userId); // Добавляем описание видео

            console.log(formData)

            // Отправляем данные на сервер
            const response = await axios.post('http://localhost:5041/Yandex/UploadFileToDisk', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            // В случае успешной загрузки выводим сообщение об успешной загрузке
            console.log('Файлы успешно загружены:', response.data);
            setUploading(false);
            setUploadSuccess(true);
        } catch (error) {
            // В случае ошибки выводим сообщение об ошибке
            setUploading(false);
            console.error('Ошибка при загрузке файлов:', error);
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
                    {/* Поле для выбора видео */}
                    <input type="file" name="selectedVideo" accept="video/*" onChange={handleFileChange}/>

                    {/* Поле для ввода ссылки на изображение */}
                    <input type="text" name="selectedImage" placeholder="Ссылка на изображение" value={videoData.selectedImage}
                           onChange={handleInputChange}/>

                    {/* Поле для ввода названия видео */}
                    <input type="text" name="videoName" placeholder="Название видео" value={videoData.videoName}
                           onChange={handleInputChange}/>

                    {/* Поле для ввода описания видео */}
                    <textarea name="videoDescription" placeholder="Описание видео" value={videoData.videoDescription}
                              onChange={handleInputChange}></textarea>

                    {/* Кнопка отправки формы */}
                    <button type="submit" disabled={uploading}>
                        {uploading ? 'Загрузка...' : 'ДОБАВИТЬ ВИДЕО'}
                    </button>
                </form>
            </div>
            {uploadSuccess && (
                <div className="upload-success-message">
                    Видео успешно загружено!
                </div>
            )}
        </>
    );
};

export default AdminAddVideo;
