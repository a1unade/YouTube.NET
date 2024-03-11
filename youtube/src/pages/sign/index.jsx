import React from "react";
import '../../assets/sign.css'
import Google_logo from "./components/Google_logo";

const Sign = () => {
    return(
        <>
            <div className="content">
                <div className="sign-container">
                    <div>
                        <Google_logo />
                        <h1>Sign in</h1>
                        <span>to continue to YouTube</span>
                    </div>
                    <div className="input-container">
                        <span>Email or phone</span>
                        <input type="email"></input>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Sign;