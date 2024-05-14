// Начальное состояние сайта
const initialState = {
    isOriginal: true
}

export const settingsReducer = (state = initialState, action) => {
    switch (action.type) {
        case 'SWITCH_SITE_TYPE':
            return {
                isOriginal: action.payload
            };
        default:
            return state;
    }
}