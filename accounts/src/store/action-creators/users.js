import {apiClient} from "../../utils/apiClient.js";

// Создание стейта пользователя по почте (при регистрации)
export const createUserByEmail = (email) => {
    return async (dispatch) => {
        try {
            const encodedEmail = encodeURIComponent(email);
            const response = await apiClient(`Auth/getUserByEmail/${encodedEmail}`);

            if (response.data.responseType === 0) {
                dispatch({
                    type: "CREATE_USER", payload: {
                        name: response.data.name,
                        surname: response.data.surname,
                        email: response.data.email,
                        gender: response.data.gender,
                        birthdate: response.data.birthdate
                    }
                });
            } else {
                console.log("Error");
            }
        } catch (e) {
            console.log(e);
        }
    }
}

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