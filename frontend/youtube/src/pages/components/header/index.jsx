import {useState} from 'react';
import {useNavigate} from 'react-router-dom';
import {Logo, Search, AddVideo} from '../../../assets/Icons.jsx';
import UserMenu from './components/UserMenu.jsx';
import {useSelector} from "react-redux";

// eslint-disable-next-line react/prop-types
const Header = ({toggleMenu}) => {
    const navigate = useNavigate()
    const [isDetailsOpen, setDetailsOpen] = useState(false);
    const [isFocused, setIsFocused] = useState(false);
    const [search, setSearch] = useState('');
    const {userId} = useSelector((state) => state.user.userId);
    const {avatar} = useSelector((state) => state.user.avatar);

    const handleBlur = () => {
        setIsFocused(false);
    };

    return (
        <>
            <div className='main-header'>
                <div style={{marginLeft: 14}} className='user-buttons'>
                    <div className='button-container'>
                        <button onClick={toggleMenu}>
                            <svg xmlns='http://www.w3.org/2000/svg' height='24' viewBox='0 0 24 24' width='24'
                                 focusable='false'>
                                <path d='M21 6H3V5h18v1zm0 5H3v1h18v-1zm0 6H3v1h18v-1z'></path>
                            </svg>
                        </button>
                    </div>
                    <div onClick={() => navigate('/')}>
                        <Logo/>
                    </div>
                </div>
                <div className='horizontal-flex-container'>
                    <input type='text' placeholder='Введите запрос' onChange={(e) => setSearch(e.target.value)}
                           value={search} className={`search-bar ${isFocused ? 'active' : ''}`} onBlur={handleBlur}/>
                    <button className='center-box-with-text'
                            onClick={() => navigate(`/search/${search.replace(/ /g, '+')}`)}>
                        <Search className='svg-container'/>
                    </button>
                </div>
                <div className='user-buttons'>
                    <div className='button-container'>
                        <button>
                        <AddVideo className='svg-container'/>
                        </button>
                    </div>
                    {
                        userId !== '' ?
                            <div style={{marginRight: 20}}>
                                <div className='dropdown'>
                                    <div className='button-container menu' style={{marginLeft: 0}}>
                                        <button onClick={() => setDetailsOpen(!isDetailsOpen)}>
                                            <img className='circular-avatar'
                                                 src={avatar}
                                                 alt=''/>
                                        </button>
                                    </div>
                                    {isDetailsOpen && (<UserMenu/>)}
                                </div>
                            </div>
                            :
                            <button className="sign-in-button"
                                    onClick={() => window.location.replace("http://localhost:5172/signin")}>Войти</button>
                    }
                </div>
            </div>
        </>
    );
}

export default Header;