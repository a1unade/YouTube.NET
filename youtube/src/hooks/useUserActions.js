import {useDispatch} from "react-redux";
import {bindActionCreators} from "redux";
import * as UserActionCreators from '../store/action-creators/users.js'
import * as SettingsActionCreators from '../store/action-creators/settings.js'

export const useUserActions = () => {
    const dispatch = useDispatch();
    return bindActionCreators(UserActionCreators, dispatch);
}

export const useSettingsActions = () => {
    const dispatch = useDispatch();
    return bindActionCreators(SettingsActionCreators, dispatch)
}