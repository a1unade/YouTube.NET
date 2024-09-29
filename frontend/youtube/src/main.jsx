import ReactDOM from 'react-dom/client';
import './index.css';
import Routing from './Routing';
import {Provider} from "react-redux";
import store from "./store"

export const App = () => {
    return (
        <>
            <Provider store={store}>
                <Routing/>
            </Provider>
        </>
    );
};

ReactDOM.createRoot(document.getElementById('root')).render(<App/>);
