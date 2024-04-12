import ChannelPage from "./ChannelPage";

const Featured = () => {
    return (
        <>
            <ChannelPage />
            <div className="main-content">
                <div className="for-you">
                    <span>
                        Для вас
                    </span>
                </div>

                <div className="video">ТУТ ВИДОСЫ </div>
                fawfawfwfwfwfwfawfokafwokff
                fllfmawfkmwf<br />
                <hr className="separator" style={{ marginTop: 20 }} />

                <div className="video-button">
                    <span>
                        Видео
                    </span>
                    <div className="reproduce">
                        <button>
                            <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24" focusable="false">
                                <path d="m7 4 12 8-12 8V4z"></path>
                            </svg>
                            Воспроизвести все
                        </button>

                    </div>

                </div>

                <div className="video">ТУТ ВИДОСЫ </div>
                <hr className="separator" style={{ marginTop: 20 }} />

                <div className="video-list">
                </div>
            </div>
        </>
    );
};
export default Featured;