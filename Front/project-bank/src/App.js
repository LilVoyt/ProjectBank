import './App.css';
import RegistrationForm from './components/RegistrationForm';
import CustomerList from './components/CustomerList';
import { fetchCustomers } from './services/customers';
import { useEffect, useState } from 'react';

function App() {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      let customers = await fetchCustomers();

      setCustomers(customers);
    }

    fetchData();
  })
  return (
    <section className='MainWindow'>
      <div className='RegAndList'>
        <RegistrationForm></RegistrationForm>
        <CustomerList customers = {customers}></CustomerList>
      </div>
    </section>
  );
}

export default App;
