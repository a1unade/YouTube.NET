import { useState } from "react";
import {useSignalR} from "../../../hooks/useSignalR.js";

// eslint-disable-next-line react/prop-types
const Confirmation = ({ email, setContainerContent, containerContent }) => {
    const [emailConfirmed, setEmailConfirmed] = useState(false);
    useSignalR(setEmailConfirmed);
    return (
        <>
            <div className="header">
                <h1 style={{ maxWidth: 300 }}>Подтвердите адрес электронной почты</h1>
                <div className="notice" style={{ marginLeft: 0, marginTop: 30, maxWidth: 350 }}>
                    {/* eslint-disable-next-line react/no-unescaped-entities */}
                    <span style={{ fontSize: 14, lineHeight: 1.5 }}>Следйуте инструкции в письме, отправленном на адрес {email}. Если письма нет во входящих, проверьте папку "Спам".</span>
                </div>
            </div>
            {
                emailConfirmed ? setContainerContent(containerContent + 1) : null
            }
        </>
    );
}

export default Confirmation;