import { useState } from 'react';
import './RegistrationForm.css';
import Header from './Header.jsx';

const RegistrationForm = ({ onRegister }) => {
  const [formData, setFormData] = useState({
    nickName: '',
    firstName: '',
    lastName: '',
    country: '',
    phoneNumber: '',
    email: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onRegister(formData);
    setFormData({
      nickName: '',
      firstName: '',
      lastName: '',
      country: '',
      phoneNumber: '',
      email: '',
    });
  };

  return (
    <>
    <Header></Header>
    <form className='registration-form' onSubmit={handleSubmit}>

      <h2>Register</h2>
      <div>
        <input
          type="text"
          name="nickName"
          value={formData.nickName}
          onChange={handleChange}
          placeholder="Nick Name"
          required
        />
      </div>
      <div>
        <input
          type="text"
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
          placeholder="First Name"
          required
        />
      </div>
      <div>
        <input
          type="text"
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
          placeholder="Last Name"
          required
        />
      </div>
      <div>
        <input
          type="text"
          name="country"
          value={formData.country}
          onChange={handleChange}
          placeholder="Country"
          required
        />
      </div>
      <div>
        <input
          type="text"
          name="phoneNumber"
          value={formData.phoneNumber}
          onChange={handleChange}
          placeholder="Phone Number"
          required
        />
      </div>
      <div>
        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          placeholder="Email"
          required
        />
      </div>
      <button type="submit">Register</button>
    </form>
    </>
  );
};

export default RegistrationForm;
