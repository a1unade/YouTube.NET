import ContentAdd from "./ContentAdd.jsx";
import VideoInfo from "./VideoInfo.jsx";

const AdminAddVideo = () => {
    const video = {
        img: "https://i.ytimg.com/vi/HbWkGxnOEXw/mqdefault.jpg",
        like: 23,
        dislike: 22,
        comment: 3,
        date: "2020-02-20",
        view: 3232,
        name : "Прятки Cs:go"
    }
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


            <div className="add-video-button">
                <button>
                    ДОБАВИТЬ ВИДЕО
                </button>
            </div>

        </>
    );
};

export default AdminAddVideo;