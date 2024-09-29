import TrendMenu from "./TrendMenu.jsx";

const InTrend = () => {
    return(
        <>
            <div className="trend-list" style={{marginLeft: 20}}>
                <div className="channel-description" style={{marginLeft: 20}}>

                    <div className='trend-image' style={{marginBottom: 0}}>
                        <img alt=""
                             className="yt-core-image yt-core-image--fill-parent-height yt-core-image--fill-parent-width yt-core-image--content-mode-scale-to-fill yt-core-image--loaded"
                             src="https://www.youtube.com/img/trending/avatar/trending.png"/>
                    </div>
                    <div className='main-description' style={{marginBottom: 0}}>
                        <h1 style={{marginTop: 16}}>В тренде</h1>
                    </div>
                </div>
            </div>
            <TrendMenu></TrendMenu>

        </>
    );
};
export default InTrend;