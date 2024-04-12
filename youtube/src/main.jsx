import React, { useState } from 'react';
import { BrowserRouter } from 'react-router-dom';
import ReactDOM from 'react-dom/client';
import './index.css';
import Routing from './Routing';
import Header from './pages/components/header/index.jsx';
import LeftMenu from './pages/components/left_menu/index.jsx';

const App = () => {
  const [isOpen, setOpen] = useState(false);

  const toggleMenu = () => {
    if (isOpen) {
      document.getElementById('page').style.marginLeft = '100px';
    } else {
      document.getElementById('page').style.marginLeft = '260px';
    }
    setOpen(!isOpen);
  };

  return (
    <>
      <BrowserRouter>
        <div>
          <LeftMenu isOpen={isOpen} />
          <Header toggleMenu={toggleMenu} />
        </div>
        <div id='page' className='routing'>
          <Routing />
        </div>
      </BrowserRouter>
    </>
  );
};

ReactDOM.createRoot(document.getElementById('root')).render(<App />);
