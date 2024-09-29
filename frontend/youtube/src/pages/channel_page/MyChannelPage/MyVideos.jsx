import {useState} from "react";
import MyChannelPage from "./MyChannelPage.jsx";



const MyVideos = () => {
    const [sortParam, setSortParam] = useState("");
    console.log(sortParam)
    return (
        <>
            <MyChannelPage/>
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
export default MyVideos;