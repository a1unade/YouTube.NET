import {useState} from 'react';
import {BrowserRouter} from 'react-router-dom';
import ReactDOM from 'react-dom/client';
import './index.css';
import Routing from './Routing';
import Header from './pages/components/header/index.jsx';
import LeftMenu from './pages/components/left_menu/index.jsx';
import {Provider} from "react-redux";
import store from "./store"

const App = () => {
    const [isOpen, setOpen] = useState(false);

    const toggleMenu = () => {
        if (isOpen) {
            document.body.classList.remove('menu-opened');
        } else {
            document.body.classList.add('menu-opened');
        }
        setOpen(!isOpen);
    };

    return (
        <>
            <Provider store={store}>
                <BrowserRouter>
                    <div>
                        <LeftMenu isOpen={isOpen}/>
                        <Header toggleMenu={toggleMenu}/>
                    </div>
                    <div id='page' className='routing'>
                        <Routing/>
                    </div>
                </BrowserRouter>
            </Provider>
        </>
    );
};

ReactDOM.createRoot(document.getElementById('root')).render(<App/>);
