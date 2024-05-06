// Root state для хука typed selector
export const UserRootState = {
    user: {
        // Имя пользователя
        name: '',
        // Фамилия пользователя
        surname: null,
        // Дата рождения
        birthdate: new Date(),
        // Пол пользователя
        gender: '',
        // Электронная почта пользователя
        email: ''
    }
};
