import ContentAdd from "./ContentAdd.jsx";
import PlaylistInfo from "./PlaylistInfo.jsx";

const AdminAddPlaylist = () => {
    const playlist = {
        img: "https://i.ytimg.com/vi/HbWkGxnOEXw/mqdefault.jpg",
        updateDate: "2021-02-23",
        videoCount : 32,
        type : "playlist",
        name : "cs:go"
    }
    return(
        <>
            <ContentAdd/>
            <div className="table-header">
                <div className="table-header-titles">
                    <span>
                        Тип
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        Доступ
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        Последнее изменение
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        Количество видео
                    </span>
                </div>
            </div>
            <hr className="separator" style={{marginTop: 10}}/>
            {!playlist
                ? <div className="all-video">
                    <div className="no-image">
                        <img alt="No content" className="style-scope ytcp-playlist-section-content"
                             src="https://www.gstatic.com/youtube/img/creator/no_content_illustration_v3.svg"/>
                    </div>
                    <div className="no-content-title">
                    <span>
                        Создайте собственный плейлист из видео, которые уже есть на
                    </span>
                        <br/>
                        <span>
                        канале, или загрузите новые ролики.
                    </span>
                    </div>
                    <div className="add-video-button">
                        <button>
                            СОЗДАТЬ ПЛЕЙЛИСТ
                        </button>
                    </div>

                </div>
                : <PlaylistInfo playlist={playlist}/>
            }

            <div className="add-video-button">
                <button>
                    СОЗДАТЬ ПЛЕЙЛИСТ
                </button>
            </div>
        </>
    );
};

export default AdminAddPlaylist;