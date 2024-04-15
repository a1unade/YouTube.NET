import Music from "./Music.jsx";

const MusicFeatured = () => {
    return(
        <>
            <Music/>
            <div className="main-content">
                <div className="music-button" style={{marginTop: 0}}>
                    <span>
                        Unique Performances
                    </span>
                    <div className="reproduce">
                        <button>
                            <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                                 focusable="false">
                                <path d="m7 4 12 8-12 8V4z"></path>
                            </svg>
                            Воспроизвести все
                        </button>

                    </div>

                </div>
            </div>

        </>
    );
};

export default MusicFeatured;