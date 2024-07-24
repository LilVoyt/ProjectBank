import { useEffect, useState } from 'preact/hooks';
import { Routes, Route, Link } from 'react-router-dom';
import './app.css';
import RegistrationForm from './components/RegistrationForm';
import CustomerList from './components/CustomerList';
import { fetchCustomers, createCustomers } from './services/customers';

export function App() {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      let customers = await fetchCustomers();
      setCustomers(customers);
    }
    fetchData();
  }, []);

  const onCreate = async (customer) => {
    await createCustomers(customer);
    let customers = await fetchCustomers();
    setCustomers(customers);
  }

  return (
    <section className='MainWindow'>
      <div id='RegAndList'>
        <Routes>
          <Route path="/" element={<RegistrationForm onRegister={onCreate} />} />
          <Route path="/customers" element={<CustomerList customers={customers} />} />
        </Routes>
      </div>
    </section>
  );
}
