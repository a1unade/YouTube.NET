// reducer for users state
//
// import { UserAction } from '../../../interfaces/user-actions-crud';
//
// // Начальное состояние пользователя
// const initialState = {
//   // id пользователя
//   id: '',
//   // Имя пользователя
//   name: '',
//   // Фамилия пользователя
//   surname: null,
//   // Электронная почта пользователя
//   email: '',
//   // Никнейм пользователя
//   username: '',
// };
//
// export const userReducer = (state = initialState, action: UserAction) => {
//   switch (action.type) {
//     case 'CREATE_USER':
//       return {
//         id: action.payload.id,
//         name: action.payload.name,
//         surname: action.payload.surname,
//         email: action.payload.email,
//         username: action.payload.username,
//       };
//     case 'UPDATE_USER_NAME':
//       return { ...state, name: action.payload };
//     case 'UPDATE_USER_SURNAME':
//       return { ...state, surname: action.payload };
//     default:
//       return state;
//   }
// };
