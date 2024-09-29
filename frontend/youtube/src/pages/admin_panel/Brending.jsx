import ChannelSettings from "./ChannelSettings.jsx";

const Brending = () => {




    return(
        <>
            <ChannelSettings/>
            <div className="channel-settings-header">
                <div className="channel-edit-container">
                    <div className="channel-settings-page">
                        <div className="channel-settings-info">
                            <h2>
                                Фото профиля
                            </h2>
                        </div>
                        <div className="channel-settings-info">
                    <span>
                        Фото профиля показывается, например, рядом с вашими видео или комментариями на YouTube.

                    </span>
                            <div className="brending-image-title">
                                <div className="brending-image">
                                    <img
                                        src="https://avatars.mds.yandex.net/i?id=90451054b55a50769ff9b7774b80781fadd09ad5-10743754-images-thumbs&ref=rim&n=33&w=289&h=250"
                                        alt=""/>
                                </div>
                                <div className="brending-title">
                                    <div className="brending-title-info">
                                        <span>
                                        Рекомендуем использовать изображение размером не менее 98 х 98 пикселей в формате PNG или GIF.
                                        Анимированные картинки загружать нельзя.
                                        Размер файла – не более 4 МБ. Помните, что изображение должно соответствовать правилам сообщества YouTube.
                                        </span>
                                    </div>
                                    <div className="channel-settings-buttons" style={{marginTop: 8}}>
                                        <div className="submit-buttons">
                                            <button>
                                                ИЗМЕНИТЬ
                                            </button>
                                            <button>
                                                УДАЛИТЬ
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div className="channel-edit-container">
                    <div className="channel-settings-page">
                        <div className="channel-settings-info">
                            <h2>
                                Баннер
                            </h2>
                        </div>
                        <div className="channel-settings-info">
                    <span>
                        Это изображение показывается в верхней части страницы канала.
                    </span>
                            <div className="brending-image-title">
                                <div className="brending-image">
                                    <svg width="290" height="160" viewBox="0 0 290 160"
                                         className="style-scope ytcp-banner-upload">
                                        <rect x="65" y="15" width="160" stroke="red"
                                              fill="red"
                                              className="style-scope ytcp-banner-upload"
                                              height="90"></rect>
                                        <image className="svg-image style-scope ytcp-banner-upload" x="65" y="15"
                                               width="160" preserveAspectRatio="none" href="" visibility="hidden"
                                               height="90"></image>
                                        <rect x="65" y="15" width="160" height="90"
                                              stroke="black" stroke-width="4"
                                              fill="transparent" className="style-scope ytcp-banner-upload"></rect>
                                        <polyline points="65, 103 225, 103"
                                                  stroke="black" stroke-width="8"
                                                  fill="black"
                                                  className="style-scope ytcp-banner-upload"></polyline>
                                        <rect x="80" y="70" width="90" stroke="red"
                                              fill="red"
                                              className="style-scope ytcp-banner-upload"
                                              height="14.894698239393891"></rect>
                                        <image className="svg-image style-scope ytcp-banner-upload" x="80" y="70"
                                               width="90"
                                               preserveAspectRatio="none" href="" visibility="hidden"
                                               height="14.894698239393891"></image>
                                        <rect x="80" y="85" width="90" height="35"
                                              stroke="white"
                                              fill="white"
                                              className="style-scope ytcp-banner-upload"></rect>
                                        <rect x="80" y="70" width="90" height="50"
                                              stroke="black" stroke-width="2"
                                              fill="transparent" className="style-scope ytcp-banner-upload"></rect>
                                        <polygon points="80, 120 170, 120 182, 140 68, 140"
                                                 stroke="black"
                                                 fill="black"
                                                 className="style-scope ytcp-banner-upload"></polygon>
                                        <polyline points="80, 120 170, 120"
                                                  stroke="black" stroke-width="1"
                                                  fill="black"
                                                  className="style-scope ytcp-banner-upload"></polyline>
                                        <polyline points="67, 142 183, 142"
                                                  stroke="black" stroke-width="4"
                                                  fill="black"
                                                  className="style-scope ytcp-banner-upload"></polyline>
                                        <polyline points="68, 140 182, 140"
                                                  stroke="black" stroke-width="1"
                                                  fill="black"
                                                  className="style-scope ytcp-banner-upload"></polyline>
                                        <rect x="180" y="80" width="26" stroke="red"
                                              fill="red"
                                              className="style-scope ytcp-banner-upload"
                                              height="7.126699366706227"></rect>
                                        <image className="svg-image style-scope ytcp-banner-upload" x="180" y="80"
                                               width="26" preserveAspectRatio="none" href=""
                                               height="7.126699366706227"></image>
                                        <rect x="180" y="86" width="26" height="39"
                                              stroke="white"
                                              fill="white"
                                              className="style-scope ytcp-banner-upload"></rect>
                                        <rect x="180" y="80" width="26" height="45"
                                              stroke="black" stroke-width="2"
                                              fill="transparent" className="style-scope ytcp-banner-upload"></rect>
                                        <polyline points="179, 125 207, 125"
                                                  stroke="black" stroke-width="6"
                                                  fill="white"
                                                  className="style-scope ytcp-banner-upload"></polyline>
                                        <polyline points="190, 125 196, 125" stroke="white"
                                                  stroke-width="1" fill="white"
                                                  className="style-scope ytcp-banner-upload"></polyline>
                                    </svg>
                                </div>
                                <div className="brending-title">
                                    <div className="brending-title-info">
                                        <span>
                                        Чтобы канал выглядел привлекательно на всех устройствах, советуем загрузить изображение размером не менее 2048 x 1152 пикс. Размер файла – не более 6 МБ.
                                        </span>
                                    </div>
                                    <div className="channel-settings-buttons" style={{marginTop: 20}}>
                                        <div className="submit-buttons" >
                                            <button>
                                                ЗАГРУЗИТЬ
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>





            </div>
        </>
    );
};

export default Brending;