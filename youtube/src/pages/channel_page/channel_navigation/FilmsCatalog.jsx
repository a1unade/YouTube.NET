import Films from "./Films.jsx";

const FilmsCatalog = () => {
    return(
        <>
            <Films/>
            <div className="catalog">
                <div className="films-catalog">
                <span>
                    Фильмы недоступны
                </span>
                </div>
            </div>

        </>
    );
};

export default FilmsCatalog;