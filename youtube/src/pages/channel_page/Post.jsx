
const Post = (post) => {

    function formatLikeCount(like) {
        if (like > 0 && like < 1000) {
            return like;
        } else if (like >= 1000 && like < 1000000) {

            let formattedCount = (like / 1000).toFixed(1); // Округляем до одного десятичного знака
            if (formattedCount.endsWith('0')) {
                formattedCount = formattedCount.slice(0, -2); // Удаляем последний символ
            } else {
                formattedCount = formattedCount.replace('.', ',');
            }
            return `${formattedCount} тыс.`;
        } else {
            let formattedCount = (like / 1000000).toFixed(2);
            if (formattedCount.endsWith('00')) {
                formattedCount = formattedCount.slice(0, -3);
            } else if (formattedCount.endsWith('0')) {
                formattedCount = formattedCount.slice(0, -1).replace('.', ',');
            } else {
                formattedCount = formattedCount.replace('.', ',');
            }
            return `${formattedCount} млн.`;
        }

    };

    return (
        <div class="community-post">
            <div class="author-img">
                <img id="img" src="https://yt3.googleusercontent.com/ytc/AIdro_k2tYx4NcwnW85j-YUwUlPwzF-aq8BN36geyN2wYw=s176-c-k-c0x00ffffff-no-rj" />
            </div>

            <div class="post-content">
                <div class="post-header">
                    <p>
                        {post.post.name}
                    </p>
                    <label>
                        {post.post.data}
                    </label>
                </div>
                <div className="post-main-description">
                    <span>
                        {post.post.description}
                    </span>
                    <img src={post.post.img}></img>

                   
                </div>


                <div class="post-footer">
                    <svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" height="24" viewBox="0 0 24 24" width="24" focusable="false" >
                        <path d="M18.77,11h-4.23l1.52-4.94C16.38,5.03,15.54,4,14.38,4c-0.58,0-1.14,0.24-1.52,0.65L7,11H3v10h4h1h9.43 c1.06,0,1.98-0.67,2.19-1.61l1.34-6C21.23,12.15,20.18,11,18.77,11z M7,20H4v-8h3V20z M19.98,13.17l-1.34,6 C18.54,19.65,18.03,20,17.43,20H8v-8.61l5.6-6.06C13.79,5.12,14.08,5,14.38,5c0.26,0,0.5,0.11,0.63,0.3 c0.07,0.1,0.15,0.26,0.09,0.47l-1.52,4.94L13.18,12h1.35h4.23c0.41,0,0.8,0.17,1.03,0.46C19.92,12.61,20.05,12.86,19.98,13.17z"></path>
                    </svg>
                    <span>
                        {formatLikeCount(post.post.likeCount)}
                    </span>

                    <svg xmlns="http://www.w3.org/2000/svg" height="24" style={{ marginRight: "23px" }} viewBox="0 0 24 24" width="24" focusable="false" >
                        <path d="M17,4h-1H6.57C5.5,4,4.59,4.67,4.38,5.61l-1.34,6C2.77,12.85,3.82,14,5.23,14h4.23l-1.52,4.94C7.62,19.97,8.46,21,9.62,21 c0.58,0,1.14-0.24,1.52-0.65L17,14h4V4H17z M10.4,19.67C10.21,19.88,9.92,20,9.62,20c-0.26,0-0.5-0.11-0.63-0.3 c-0.07-0.1-0.15-0.26-0.09-0.47l1.52-4.94l0.4-1.29H9.46H5.23c-0.41,0-0.8-0.17-1.03-0.46c-0.12-0.15-0.25-0.4-0.18-0.72l1.34-6 C5.46,5.35,5.97,5,6.57,5H16v8.61L10.4,19.67z M20,13h-3V5h3V13z"></path>
                    </svg>
                    <span style={{ display: "none" }}>
                        {post.post.disLike}
                    </span>

                    <svg xmlns="http://www.w3.org/2000/svg" height="16" viewBox="0 0 16 16" width="16" focusable="false" >
                        <path d="M12 3v8.59l-1.29-1.3-.3-.29H4V3h8m1-1H3v9h7l3 3V2ZM9 7H5v1h4Zm2-2H5v1h6Z"></path>
                    </svg>
                    <span>
                        {post.post.comments}
                    </span>
                </div>
            </div>
        </div>


    );
};

export default Post;

