import MenuForFilms from "./MenuForFilms.jsx";

const Films = () => {
    return(
        <>
            <div className="main-page">
                <div className="film" style={{marginLeft: 20}}>
                    <div className="channel-description" style={{marginLeft: 20}}>

                        <div className='main-image' style={{marginBottom: 0}}>
                            <img alt="" style={{width: 72, height: 72}}
                                 className="yt-core-image yt-core-image--fill-parent-height yt-core-image--fill-parent-width yt-core-image--content-mode-scale-to-fill yt-core-image--loaded"
                                 src="https://www.gstatic.com/youtube/img/tvfilm/clapperboard_profile.png"/>
                        </div>
                        <div className='main-description' style={{marginBottom: 0}}>
                            <h1 style={{marginTop: 32}}>Фильмы</h1>
                        </div>

                    </div>
                    <MenuForFilms/>
                </div>
            </div>

        </>
    );
};

export default Films;