import {apiClient} from "../../utils/apiClient.js";

export const updateUserName = (name) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: "UPDATE_USER_NAME", payload: {
                    name: name
                }
            });
        } catch (e) {
            console.log(e);
        }
    }
}

export const updateUserSurname = (surname) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: "UPDATE_USER_SURNAME", payload: {
                    surname: surname
                }
            });
        } catch (e) {
            console.log(e);
        }
    }
}

export const updateUserGender = (gender) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: "UPDATE_USER_GENDER", payload: {
                    gender: gender
                }
            });
        } catch (e) {
            console.log(e);
        }
    }
}

export const updateUserBirthdate = (birthdate) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: "UPDATE_USER_BIRTHDATE", payload: {
                    birthdate: birthdate
                }
            });
        } catch (e) {
            console.log(e);
        }
    }
}

export const updateUserEmail = (email) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: "UPDATE_USER_EMAIL", payload: {
                    email: email
                }
            });
        } catch (e) {
            console.log(e);
        }
    }
}

export const updateUserId = (userId) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: "UPDATE_USER_ID", payload: {
                    id: userId
                }
            });
        } catch (e) {
            console.log(e);
        }
    }
}