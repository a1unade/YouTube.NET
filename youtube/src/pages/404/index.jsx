import notfound from "../../assets/img/404.png";

const Index = () => {
    return (
        <div className="main-page">
            <div className="error-container">
                <img width={'100%'} height={'100%'} src={notfound} alt=""/>
                <h1>NOT FOUND</h1>
            </div>
        </div>
    );
};

export default Index;