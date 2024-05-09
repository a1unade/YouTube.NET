import {useNavigate, useParams} from "react-router-dom";
import {useEffect} from "react";
import {useUserActions} from "../../hooks/useUserActions.js";

const Auth = () => {
    const navigate = useNavigate();
    const {userId} = useParams();
    const {createUserById} = useUserActions();
    useEffect( () => {
        createUserById(userId);
        navigate("/");
    }, [userId]);

    return (
        <div>
            Loading...
        </div>
    );
};

export default Auth;