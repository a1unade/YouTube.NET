import Google_logo from "../components/Google_logo.jsx";
import Footer from "../components/Footer.jsx";

const Error = () => {
    return (
        <div className="content">
            <div className="sign-container">
                <Google_logo/>
                <div className="header">
                    <h1 style={{maxWidth: 300}}>Ошибка</h1>
                    <div className="notice" style={{marginLeft: 0, marginTop: 30, maxWidth: 350}}>
                        {/* eslint-disable-next-line react/no-unescaped-entities */}
                        <span style={{fontSize: 14, lineHeight: 1.5}}>
                        Произошла ошибка, повторите попытку позже</span>
                    </div>
                </div>
            </div>
            <Footer/>
        </div>
    );
};

export default Error;