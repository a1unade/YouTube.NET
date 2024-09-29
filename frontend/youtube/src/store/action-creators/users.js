import backendClient from "../../utils/backendClient.js";

// Создание стейта пользователя по почте (при регистрации)
export const createUserById = (userId) => {
    return async (dispatch) => {
        try {
            const response = await backendClient(`Auth/getUserById?userId=${userId}`);

            if (response.data.responseType === 0) {
                dispatch({
                    type: "CREATE_USER", payload: {
                        userId: response.data.userId,
                        name: response.data.name,
                        surname: response.data.surname,
                        email: response.data.email,
                        premium: response.data.premium
                    }
                });
            } else {
                console.log("error");
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





