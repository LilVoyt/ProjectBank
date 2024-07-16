import './App.css';
import RegistrationForm from './components/RegistrationForm';
import CustomerList from './components/CustomerList';

function App() {
  return (
    <section className='MainWindow'>
      <div className='RegAndList'>
        <RegistrationForm></RegistrationForm>
        <CustomerList></CustomerList>
      </div>
    </section>
  );
}

export default App;
