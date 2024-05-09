import {configureStore} from '@reduxjs/toolkit';
import {userReducer} from "./reducers/user/userReducer.js";


const store = configureStore({
    reducer: userReducer
});

export default store;