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
      <div id='RegAndList'>
        <Routes>
          <Route path="/" element={<RegistrationForm />} />
          <Route path="/customers" element={<CustomerList customers={customers} />} />
        </Routes>
      </div>
    </section>
  );
}
