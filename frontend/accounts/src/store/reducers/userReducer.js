// Начальное состояние пользователя
const initialState = {
    id: "",
    // Имя пользователя
    name: "",
    // Фамилия пользователя
    surname: null,
    // Дата рождения
    birthdate: null,
    // Пол пользователя
    gender: "",
    // Электронная почта пользователя
    email: ""
}

export const userReducer = (state = initialState, action) => {
    switch (action.type){
        case 'CREATE_USER': // Создание нового пользователя и сохранение в стейт
            return {
                name: action.payload.name,
                surname: action.payload.surname,
                birthdate: action.payload.birthdate,
                gender: action.payload.gender,
                email: action.payload.email,
            };
        case 'UPDATE_USER_NAME':
            return  {...state, name: action.payload};
        case 'UPDATE_USER_EMAIL':
            return  {...state, email: action.payload};
        case 'UPDATE_USER_SURNAME':
            return  {...state, surname: action.payload};
        case 'UPDATE_USER_GENDER':
            return  {...state, gender: action.payload};
        case 'UPDATE_USER_BIRTHDATE':
            return  {...state, birthdate: action.payload};
        case 'UPDATE_USER_ID':
            return  {...state, id: action.payload};
        default:
            return state
    }
}