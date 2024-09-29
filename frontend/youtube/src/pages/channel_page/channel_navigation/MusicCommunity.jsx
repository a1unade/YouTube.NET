import Music from "./Music.jsx";
import Post from "../Post.jsx";

const MusicCommunity = () => {
    let posts = {
        authorimg: "https://yt3.googleusercontent.com/vCqmJ7cdUYpvR0bqLpWIe8ktaor4QafQLlfQyTuZy-M9W_YafT8Wo9kdsKL2St1BrkMRpVSJgA=s176-c-k-c0x00ffffff-no-rj",
        name: "Яцкич",
        description: "Крутой видос вышел у меня на канале Метро бум ",
        img : "https://avatars.mds.yandex.net/i?id=98275c915af60f1493744b447e4b9a4e057d1212-10808639-images-thumbs&n=13",
        likeCount: 9537,
        disLike: 123,
        comments: 213,
        data: "21.02.2019"
    }
    console.log(posts)

    let post = {
        authorimg: "https://yt3.googleusercontent.com/vCqmJ7cdUYpvR0bqLpWIe8ktaor4QafQLlfQyTuZy-M9W_YafT8Wo9kdsKL2St1BrkMRpVSJgA=s176-c-k-c0x00ffffff-no-rj",
        name: "Яцкич",
        description: "Завтра новый видос",
        img : "https://static.zerochan.net/Matsumoto.Rangiku.full.3901367.jpg",
        likeCount: 23231,
        disLike: 123,
        comments: 12,
        data: "21.02.2024"
    }
    return(
        <>
            <Music/>


            <div className="main-content">
                <div className="post-list">
                    <Post post={posts}/>
                    <Post post={post}/>
                </div>
            </div>
        </>
    )
}
export default MusicCommunity;