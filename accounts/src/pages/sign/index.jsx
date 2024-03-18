import React, { useState } from "react";
import '../../assets/sign.css';
import Google_logo from '../components/Google_logo';
import Email from "./components/Email";
import Password from "./components/Password";
import Footer from "../components/Footer";

const Sign = () => {
    const [containerContent, setContainerContent] = useState(0);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const renderContainerContent = () => {
        switch (containerContent) {
            case 0:
                return (
                    <Email setEmail={setEmail} email={email} setContainerContent={setContainerContent} containerContent={containerContent} />
                );
            case 1:
                return (
                    <Password setContainerContent={setContainerContent} containerContent={containerContent} setPassword={setPassword} email={email} password={password} />
                );
            default:
                return null;
        }
    }

    return (
        <>
            <div className="content">
                <div className="sign-container">
                    <Google_logo />
                    {renderContainerContent()}
                </div>
                <Footer />
            </div>
        </>
    );
}

export default Sign;