const PlaylistInfo = ({playlist}) => {
    return(
        <>
            <div className="table-header">
                <div className="video-info-titles">
                    <div className="video-info">
                        <img src={playlist.img} style={{width: 120, height: 68}}/>
                        <div className="video-info-name">
                            <span>
                                {playlist.name}
                            </span>
                        </div>
                    </div>
                </div>

                <div className="table-header-titles">
                    <span>
                        {playlist.type}
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        {playlist.type}
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        {playlist.videoCount}
                    </span>
                </div>
                <div className="table-header-titles">
                    <span>
                        {playlist.videoCount}
                    </span>
                </div>
            </div>
            <hr className="separator" style={{marginTop: 10}}/>

        </>
    );
};

export default PlaylistInfo;