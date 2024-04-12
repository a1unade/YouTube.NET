import Menu from './Menu';
import { useParams } from 'react-router-dom';
import AboutChannel from './AboutChannel';
import { useEffect, useState } from "react";
import apiClient from "../../utils/apiClient.js";


const ChannelPage = () => {

  const { customUrl } = useParams(); // Извлечение параметра name из URL
  const [isFollow, setIsFollow] = useState(false);
  const [owners, setOwner] = useState([])

  const [isImageBig, setIsImageBig] = useState(false);


  const titles = ['подписчик', 'подписчика', 'подписчиков'];
  const cases = [2, 0, 1, 1, 1, 2];


  useEffect(() => {
    apiClient.get(`/channels?part=snippet&part=statistics&part=brandingSettings&part=contentDetails&forHandle=${customUrl}&maxResults=5`)
      .then(response => {
        console.log(response.data.items[0])
        setOwner(response.data.items[0]);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);


  function formatFollowersCount(count) {
    if (count < 1000 && count >= 0) {
      const index = count % 100 > 4 && count % 100 < 20 ? 2 : cases[Math.min(count % 10, 5)];
      return `${count} ${titles[index]}`;

    } else if (count >= 1000 && count < 1000000) {

      let formattedCount = (count / 1000).toFixed(1); // Округляем до одного десятичного знака
      if (formattedCount.endsWith('0')) {
        formattedCount = formattedCount.slice(0, -2); // Удаляем последний символ
      } else {
        formattedCount = formattedCount.replace('.', ',');
      }
      return `${formattedCount} тыс. ${titles[1]}`;

    } else if (count >= 1000000 && count <= 999999999) {

      let formattedCount = (count / 1000000).toFixed(2);
      if (formattedCount.endsWith('00')) {
        formattedCount = formattedCount.slice(0, -3);
      } else if (formattedCount.endsWith('0')) {
        formattedCount = formattedCount.slice(0, -1).replace('.', ',');
      } else {
        formattedCount = formattedCount.replace('.', ',');
      }
      return `${formattedCount} млн. ${titles[2]}`;
    } else {
      // Для чисел больше 999 999 999 (миллиардов) или меньше 0
      return 'Invalid count';
    }
  }
  const handleImageClick = () => {
    setIsImageBig(!isImageBig);
  }
  if (owners === null || owners.length === 0) {
    return (
      <div></div>
    );
  } else {
    return (
      <>
        <div className='main-page' style={{ marginLeft: 40 }}>
          <div className="channel-page">
            {isImageBig ? <div className='dark-overlay'></div> : null}
            {
              owners.brandingSettings.image && owners.brandingSettings.image.bannerExternalUrl !== null
                ? <div className="channel-header">
                  <img src={owners.brandingSettings.image.bannerExternalUrl + "=w1707-fcrop64=1,00005a57ffffa5a8-k-c0xffffffff-no-nd-rj"} alt="Channel Banner" />
                </div>

                : <div></div>
            }
            <div className='channel-description'>
              <div className='main-image'>
                <img id="img" draggable="false" className="style-scope yt-img-shadow" alt="" src={owners.snippet.thumbnails.high.url} />
              </div>
              <div className='main-description'>
                <h1>{owners.snippet.title}</h1>

                <div className='owner-info'>
                  <span >{owners.snippet.customUrl}</span>
                  <span className="dot"> . </span>
                  <span>{formatFollowersCount(owners.statistics.subscriberCount)}</span>
                  <span className="dot"> . </span>
                  <span>{owners.statistics.videoCount} видео</span>
                </div>
                <div className='owner-description'>
                  <span>{owners.snippet.description.length > 80
                    ? owners.snippet.description.slice(0, 79) + "..."
                    : owners.snippet.description}
                  </span>
                  <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24" focusable="false" onClick={handleImageClick}>
                    <path d="m9.4 18.4-.7-.7 5.6-5.6-5.7-5.7.7-.7 6.4 6.4-6.3 6.3z"></path>
                  </svg>
                  {isImageBig && <AboutChannel onClose={() => setIsImageBig(false)} owners={owners} />}
                </div>
                <div className='subscription'>
                  {isFollow
                    ? <button id='isFollow' onClick={() => setIsFollow(false)}>Подписаться</button>
                    : <button id='noFollow' onClick={() => setIsFollow(true)}>
                      <img src="notification.png" />Вы подписаны
                    </button>
                  }
                </div>
              </div>
            </div>
          </div>
          <Menu channelName={owners.snippet.customUrl} />
        </div>

      </>
    );
  }
};

export default ChannelPage; 
