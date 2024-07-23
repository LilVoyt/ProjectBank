import { Link } from 'react-router-dom';
import './Header.css'

const Header = () => {
    return (
        <>
            <nav className='top-menu'>
                <div className='left-menu'>
                    <h1>Project Bank</h1>
                    <ul>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/customers">Customers</Link></li>
                    </ul>
                </div>
                <div className='right-menu'>
                    <button>login</button>
                </div>
            </nav>
        </>
    )
};

export default Header;
