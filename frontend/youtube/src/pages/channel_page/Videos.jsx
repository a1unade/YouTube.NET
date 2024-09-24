import ChannelPage from "./ChannelPage";
import {useState} from "react";



const Videos = () => {
    const [sortParam, setSortParam] = useState("");
    console.log(sortParam)
    return (
        <>
            <ChannelPage/>
            <div className="main-content">
                <div className="sort-video-buttons">
                    <button autoFocus onClick={() => setSortParam("new")}>
                        Новые
                    </button>
                    <button onClick={() => setSortParam("popular")}>
                        Популярные
                    </button>
                    <button onClick={() => setSortParam("old")}>
                        Старые
                    </button>

                </div>
            </div>
            
        
        </>
    )
};
export default Videos;