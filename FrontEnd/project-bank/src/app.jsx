import { useEffect, useState } from 'preact/hooks'
import './app.css'
import ReagistationForm from './components/RegistrationForm'
import CustomerList from './components/CustomerList';
import { fetchCustomers } from './services/customers';
// import { useEffect, useState } from 'react';

export function App() {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      let customers = await fetchCustomers();

      setCustomers(customers);
    }

    fetchData();
  }, [])

  return (
    <section className='MainWindow'>
      <div className='RegAndList'>
        <ReagistationForm></ReagistationForm>
        <CustomerList customers={customers}></CustomerList>
      </div>
    </section>
  )
}
