// Начальное состояние пользователя
const initialState = {
    // Имя пользователя
    name: "",
    // Фамилия пользователя
    surname: null,
    // Электронная почта пользователя
    email: ""
}

export const userReducer = (state = initialState, action) => {
    switch (action.type){
        case 'CREATE_USER': // Создание нового пользователя и сохранение в стейт
            return {
                name: action.payload.name,
                surname: action.payload.surname,
                email: action.payload.email,
            };
        case 'UPDATE_USER_NAME':
            return  {...state, name: action.payload};
        case 'UPDATE_USER_SURNAME':
            return  {...state, surname: action.payload};
        default:
            return state
    }
}