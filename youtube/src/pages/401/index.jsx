import unauthorized from "../../assets/img/error_401.jpg";

const Index = () => {
    return (
        <div className="main-page">
            <div className="error-container">
                <img width={'100%'} height={'100%'} src={unauthorized} alt=""/>
                <h1>UNAUTHORIZED</h1>
            </div>
        </div>
    );
};

export default Index;