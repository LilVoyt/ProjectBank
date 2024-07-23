import './CustomerList.css';
import Header from './Header.jsx';

const CustomerList = ({ customers }) => {
  return (
    <>
    <Header></Header>
    <div className='customer-list'>
      <h2>Registered Users</h2>
      <ul>
        {customers.map((customer, index) => (
          <li key={index}>
            <div>ID: {customer.id}</div>
            <div>Name: {customer.name}</div>
            <div>Last Name: {customer.lastName}</div>
            <div>Country: {customer.country}</div>
            <div>Phone: {customer.phone}</div>
            <div>Email: {customer.email}</div>
          </li>
        ))}
      </ul>
    </div>
    </>
  );
};

export default CustomerList;

