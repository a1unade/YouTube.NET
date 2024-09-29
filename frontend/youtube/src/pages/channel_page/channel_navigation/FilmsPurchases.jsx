import Films from "./Films.jsx";

const FilmsPurchases = () => {
    return (
        <>
            <Films></Films>

            <div className="films-container">

                <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24" focusable="false" style={{width: 120, height: 120}}>
                    <g>
                        <path d="M5.02 6.75c-.14-.82.42-1.59 1.23-1.73s1.59.41 1.73 1.23c.14.82-.41 1.59-1.23 1.73-.82.14-1.59-.42-1.73-1.23zM3.99 4 4 11.08l9.36 9.36 7.07-7.07-9.36-9.36L3.99 4m-1-1 8.49.01 10.36 10.36-8.49 8.49L3 11.49 2.99 3z"></path>
                    </g>
                </svg>
                <div className="films-message">
                    <h2>У вас пока нет покупок</h2>
                    <span>Здесь появятся фильмы и сериалы, которые вы купите или возьмете напрокат.</span>
                </div>
            </div>
        </>
    );
};

export default FilmsPurchases;