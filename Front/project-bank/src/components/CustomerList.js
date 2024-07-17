import React from 'react';
import './CustomerList.css'

const CustomerList = ({customer}) => {
  return (
    <div className='customer-list'>
      <h2>Registered Users</h2>
      <ul>
        {/* {customer.map((n) => {
            <li key={n.id}>

            </li>
        })} */}
      </ul>
    </div>
  );
};

export default CustomerList;
