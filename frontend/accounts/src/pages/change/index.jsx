import React, {useState} from 'react';
import Google_logo from "../components/Google_logo.jsx";
import Footer from "../components/Footer.jsx";
import {useNavigate, useParams} from "react-router-dom";
import {apiClient} from "../../utils/apiClient.js";
import {avatarData} from "../../data/avatarData.js";

const ChangeAvatar = () => {
    const userId = useParams();
    const navigate = useNavigate();
    const [avatarId, setAvatarId] = useState(1);
    console.log(userId)

    const changeAvatar = async () => {
        try{
            const response = await apiClient.post('Auth/changeAvatar', {
                userId: userId.id,
                avatarId: avatarId
            });
            console.log(response.data);

            if(response.data.type === 0){
                window.location.replace(`http://localhost:5173/auth/${userId}`);
            } else{
              navigate("/error");
            }
        }
        catch(error){
            console.log(error);
            navigate("/error");
        }
    }

    const selectAvatar = (id) => {
        const items = document.getElementsByClassName("profile-avatar-active");
        for (let i = 0; i < items.length; i++) {
            items[i].classList.remove("profile-avatar-active");
        }

        setAvatarId(id);
        document.getElementById(`avatar-${id}`).classList.add("profile-avatar-active");
    }

    return (
        <div className="content">
            <div className="sign-container">
                <Google_logo/>
                <div className="header">
                    <h1 style={{maxWidth: 300}}>Смена аватарки</h1>
                    <div className="notice" style={{marginLeft: 0, marginTop: 30, maxWidth: 350}}>
                        {/* eslint-disable-next-line react/no-unescaped-entities */}
                        <span style={{fontSize: 14, lineHeight: 1.5}}>Выберите новую аватарку: </span>
                        <div className="profile-avatars">
                            {avatarData.map((avatar) =>
                                <div className="profile-avatar" onClick={() => selectAvatar(avatar.id)} key={avatar.id} id={`avatar-${avatar.id}`}>
                                    <img src={avatar.img} alt="" />
                                </div>
                            )}
                        </div>
                    </div>
                </div>
                <div className="sign-buttons">
                    <button className="left-button" onClick={() => window.location.replace(`http://localhost:5173/auth/${userId}`)}>Отмена</button>
                    <button className="right-button" onClick={async () => await changeAvatar()}>Подтвердить</button>
                </div>
            </div>
            <Footer/>
        </div>
    );
};

export default ChangeAvatar;