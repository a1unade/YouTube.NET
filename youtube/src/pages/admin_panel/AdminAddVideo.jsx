import ContentAdd from "./ContentAdd.jsx";
import VideoInfo from "./VideoInfo.jsx";
import {useState} from "react";
import axios from "axios";

const AdminAddVideo = () => {

    const [selectedFile, setSelectedFile] = useState(null);
    const [uploading, setUploading] = useState(false);
    const [uploadSuccess, setUploadSuccess] = useState(false);
    const handleFileChange = (event) => {
        console.log(event.target.files[0])
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




        try {
            const formData = new FormData();
            formData.append('video',selectedFile);

            const response = await axios.post('http://localhost:5041/Yandex/UploadFileToDisk\n', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            console.log('Файл успешно загружен:', response.data);
            setUploading(true)
            setUploadSuccess(true)
        } catch (error) {
            setUploading(false)
            console.error('Ошибка при загрузке файла:', error);
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
            {/*<video src="https://downloader.disk.yandex.ru/disk/c6631babf2ba5612b7b91995e4bcc96584a95b023ee9f4c74b16d7f4a381b3a9/6639844d/fKqInKw3d7bLFOeFnMGnhDH1Z-voPfuALnTn4H9kzgjwczVfmnswgCs8yOJFDNoJUxvr-16KqQ0TCJVok7b3UwuktwEMYuLTXrZrBbdqSkKr8npumZHI4midPdWhecNq?uid=1130000064761911&filename=%D0%B0%D1%83%D1%86%D0%B0%D1%83%D1%86&disposition=attachment&hash=&limit=0&content_type=video%2Fmp4&owner_uid=1130000064761911&fsize=1479412&hid=732514b69dea9ed802bbac80e37b48f7&media_type=video&tknv=v2&etag=065b70582b558a310e3aeec7b1befaa7"/>*/}
        </>
    );
};

export default AdminAddVideo;