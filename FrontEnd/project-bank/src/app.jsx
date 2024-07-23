// src/App.jsx
import { useEffect, useState } from 'preact/hooks';
import { Routes, Route, Link } from 'react-router-dom';
import './app.css';
import RegistrationForm from './components/RegistrationForm';
import CustomerList from './components/CustomerList';
import { fetchCustomers } from './services/customers';

export function App() {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      let customers = await fetchCustomers();
      setCustomers(customers);
    }
    fetchData();
  }, []);

  return (
    <section className='MainWindow'>
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
      <div className='RegAndList'>
        <Routes>
          <Route path="/" element={<RegistrationForm />} />
          <Route path="/customers" element={<CustomerList customers={customers} />} />
        </Routes>
      </div>
    </section>
  );
}
