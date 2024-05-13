
import MyPost from "./MyPost.jsx";
import MyChannelPage from "./MyChannelPage.jsx";
const MyCommunity = () => {
    let posts = {
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
        name: "Яцкич",
        description: "Завтра новый видос",
        img : "https://static.zerochan.net/Matsumoto.Rangiku.full.3901367.jpg",
        likeCount: 23231,
        disLike: 123,
        comments: 12,
        data: "21.02.2024"
    }


console.log(posts)
    return (
        <>
            <MyChannelPage/>
            <div className="main-content">
                <div className="post-list">
                    <MyPost post={posts}/>
                    <MyPost post={post}/>
                </div>
            </div>
        </>
    )
};
export default MyCommunity;