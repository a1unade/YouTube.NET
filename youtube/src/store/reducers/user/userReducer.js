// Начальное состояние пользователя
const initialState = {
    // Id пользователя
    userId: '',
    // Имя пользователя
    name: '',
    // Фамилия пользователя
    surname: null,
    // Электронная почта пользователя
    email: '',
    // Подписка пользователя (true/false)
    premium: false,
    // Id канала пользователя
    channelId: '',
    // Аватарка пользователя
    avatar: ''
}

export const userReducer = (state = initialState, action) => {
    switch (action.type) {
        case 'CREATE_USER': // Создание нового пользователя и сохранение в стейт
            return {
                userId: action.payload.id,
                name: action.payload.name,
                surname: action.payload.surname,
                email: action.payload.email,
                premium: action.payload.premium,
                avatar: action.payload.avatar
            };
        case 'UPDATE_USER_NAME':
            return {...state, name: action.payload};
        case 'UPDATE_USER_SURNAME':
            return {...state, surname: action.payload};
        case 'UPDATE_USER_PREMIUM':
            return {...state, premium: action.payload};
        default:
            return state;
    }
}