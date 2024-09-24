import MyChannelPage from "./MyChannelPage.jsx";


const Playlists = () => {
    return (
        <>
            <MyChannelPage/>
            <div className="main-content">
                <div className="full-playlist">
                    <span>
                        Все плейлисты
                    </span>

                    <button>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24" focusable="false" >
                            <path d="M21 6H3V5h18v1zm-6 5H3v1h12v-1zm-6 6H3v1h6v-1z"></path>
                        </svg>
                        <span>
                            Упорядочить
                        </span>
                    </button>

                </div>

            </div>
        </>
    )
};
export default Playlists;