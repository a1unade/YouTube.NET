import {combineReducers} from "redux";
import {userReducer} from "./user/userReducer.js"
import {settingsReducer} from "./settings/settingsReducer.js";


export const rootReducer = combineReducers({
    user: userReducer,
    settings: settingsReducer
})