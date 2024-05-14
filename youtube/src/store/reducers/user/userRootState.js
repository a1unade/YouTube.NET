// Root state для хука typed selector
export const UserRootState = {
    user: {
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
};
