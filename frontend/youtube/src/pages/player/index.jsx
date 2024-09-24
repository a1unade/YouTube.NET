import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import apiClient from "../../utils/apiClient";
import Video from "../components/video/index.jsx";
import Description from "./components/Description.jsx";
import { formatDate, formatViews } from "../../utils/formatFunctions.js";
import { ButtonDislikeIcon, ButtonLikeIcon } from "../../assets/Icons.jsx";
import Comment from "./components/Comment.jsx";
import MapModal from "./components/MapModal.jsx";
import {useSelector} from "react-redux";
import ad from "../../assets/img/cyberad.jpeg";

const Player = () => {
  const navigate = useNavigate();
  const [videos, setVideos] = useState([]);
  const [comments, setComments] = useState([]);
  const [video, setVideo] = useState(null);
  const [channel, setChannel] = useState(null);
  const { id } = useParams();
  const [subscribe, setSubscribe] = useState(true);
  const [isFocused, setIsFocused] = useState(false);
  const {premium} = useSelector(state => state.user.premium);

  const handleViewLocationClick = (event) => {
    event.preventDefault();
    setShowMap(true);
  };
  const [showMap, setShowMap] = useState(false);
  const apiKey = import.meta.env.VITE_YANDEX_API_KEY; //KeyApi
  const locationName = "RUS";

  const handleFocus = () => {
    setIsFocused(true);
  };
  console.log(id);
  const handleCancel = () => {
    setIsFocused(false);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const videoResponse = await apiClient.get(
          `/videos?part=snippet&part=statistics&id=${id}`
        );
        setVideo(videoResponse.data.items[0]);

        const channelResponse = await apiClient.get(
          `/channels?part=snippet&part=brandingSettings&part=statistics&id=${videoResponse.data.items[0].snippet.channelId}`
        );
        setChannel(channelResponse.data.items[0]);

        const commentsResponse = await apiClient.get(
          `/commentThreads?part=snippet&videoId=${id}&maxResults=20`
        );
        setComments(commentsResponse.data.items);

        const videoListResponse = await apiClient.get(
          `/videos?part=id&chart=mostPopular&regionCode=RU&maxResults=20`
        );
        console.log(videoListResponse.data);
        setVideos(videoListResponse.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, [id]);

  if (videos.length === 0 || !video || !channel) {
    return <div></div>;
  } else {
    return (
        <>
          <div className="ad" style={{marginBottom: 40}}>
            <div>
              <img src={ad} alt="ad"/>
              <p style={{fontSize: 12, margin: 0, marginTop: 10, textAlign: 'left'}}>* Чтобы отключить
                рекламу, купите подписку Premium</p>
            </div>
            <div style={{display: 'flex', flexDirection: 'column', justifyContent: 'space-between'}}>
                            <span>Вы играете за V, наёмника в поисках уникального устройства, позволяющего обрести бессмертие.
                                Вы сможете менять киберимпланты, навыки и стиль игры своего персонажа, исследуя огромный открытый мир, где ваши поступки влияют на ход сюжета и всё, что вас окружает.</span>
              <b>
                <p>В комплект издания входит:</p>
                <i style={{textAlign: "left"}}>
                  <ul>
                    <li>Игра Cyberpunk 2077;</li>
                    <li>Масштабное обновление 2.0, полностью переработавшим различные аспекты
                      игры;
                    </li>
                    <li>Код на загрузку дополнения Phantom Liberty («Призрачная свобода»).</li>
                  </ul>
                </i>
              </b>
            </div>
          </div>
          <div className="content">
            <div className="player-page">
              <div className="player-container">
                <iframe
                    style={{borderRadius: 12}}
                    width="1080"
                    height="720"
                    src={`https://www.youtube.com/embed/${id}?autoplay=0`}
                    frameBorder={0}
                    allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                    allowFullScreen={true}
                ></iframe>
                <div className="player-video-title">
                  <p>{video.snippet.title}</p>
                </div>
                <div className="video-actions-list">
                  <div className="player-channel-info">
                    <div
                        className="main-video-info"
                        title={video.snippet.localized.title}
                    >
                      <div style={{marginRight: 20}}>
                        <div
                            className="author-image"
                            onClick={() =>
                                navigate(`/channel/${channel.snippet.customUrl}`)
                            }
                        >
                          <img
                              src={channel.snippet.thumbnails.default.url}
                              alt=""
                          />
                        </div>
                      </div>
                      <div className="main-video-details">
                        <div className="main-video-name">
                        <span>
                          <b>{video.snippet.channelTitle}</b>
                        </span>
                        </div>
                        <div className="info">
                        <span>
                          {formatViews(
                              channel.statistics.subscriberCount,
                              "followers"
                          )}
                        </span>
                        </div>
                      </div>
                    </div>
                    <button
                        id={
                            subscribe.toString().charAt(0).toUpperCase() +
                            subscribe.toString().slice(1)
                        }
                        onClick={() => setSubscribe(!subscribe)}
                        className="subscribe-button"
                    >
                      {subscribe ? "Подписаться" : "Отписаться"}
                    </button>
                  </div>
                  <div className="player-like-dislike-actions">
                    <button className="player-like-button" id="like-button">
                      <ButtonLikeIcon/>
                      <span>
                      {formatViews(video.statistics.likeCount, "likes")}
                    </span>
                    </button>
                    <button className="player-dislike-button">
                      <ButtonDislikeIcon/>
                    </button>
                  </div>
                </div>
                <div className="video-description">
                  <p>
                    {formatViews(video.statistics.viewCount, "views")}{" "}
                    {formatDate(video.snippet.publishedAt)}
                  </p>
                  <Description
                      description={video.snippet.description}
                      onViewLocationClick={handleViewLocationClick}
                  />
                </div>
                <div className="player-comments-list">
                  <h2>Комментарии: {video.statistics.commentCount}</h2>
                  <div className="player-leave-comment-section">
                    <img
                        style={{width: 40, height: 40}}
                        className="circular-avatar"
                        src="https://sm.ign.com/ign_nordic/cover/a/avatar-gen/avatar-generations_prsz.jpg"
                        alt=""
                    />
                    <div className="player-input-section">
                    <textarea
                        onFocus={handleFocus}
                        onBlur={() => setIsFocused(false)}
                        placeholder="Введите комментарий"
                    />
                      {isFocused && (
                          <div className="player-comment-buttons">
                            <button
                                id="True"
                                className="subscribe-button"
                                onClick={handleCancel}
                            >
                              Отмена
                            </button>
                            <button id="True" className="subscribe-button">
                              Оставить комментарий
                            </button>
                          </div>
                      )}
                    </div>
                  </div>
                  {comments.map((comment) => (
                      <Comment
                          key={comment.id}
                          comment={comment.snippet.topLevelComment}
                      />
                  ))}
                </div>
              </div>

              <div className="video-recommendations">
                <div className="videos-list">
                  {videos.items.map((video) => (
                      <Video id={video.id} key={video.etag}/>
                  ))}
                </div>
              </div>
            </div>
          </div>
          {showMap && (
              <MapModal
                  apiKey={apiKey}
                  locationName={locationName}
                  onClose={() => setShowMap(false)}
                  channelUrl={channel.snippet.customUrl}
                  id={id}
              />
          )}
        </>
    );
  }
};

export default Player;
