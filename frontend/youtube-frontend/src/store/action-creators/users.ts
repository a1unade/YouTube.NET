// action creators for users state redux toolkit
//
// import apiClient from '../../utils/apiClient.ts';
// import { Dispatch } from 'redux';
//
// interface ApiResponse {
//   responseType: number;
//   name: string;
//   surname: string;
//   email: string;
//   username: string;
// }
//
// // Создание стейта пользователя по почте (при регистрации)
// export const createUserById = (userId: string) => {
//   return async (dispatch: Dispatch) => {
//     try {
//       const response = await apiClient<ApiResponse>(`Auth/getUserById/${userId}`);
//
//       if (response.data.responseType === 0) {
//         dispatch({
//           type: 'CREATE_USER',
//           id: userId,
//           name: response.data.name,
//           surname: response.data.surname,
//           email: response.data.email,
//           username: response.data.username,
//         });
//       } else {
//         console.log('error');
//       }
//     } catch (e) {
//       console.log(e);
//     }
//   };
// };
