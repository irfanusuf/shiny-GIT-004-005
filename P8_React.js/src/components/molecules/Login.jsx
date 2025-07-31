import React, { useState } from 'react';

const LoginForm = ({setShowRegister , darkMode , setDarkMode }) => {



  const [form, setForm] = useState({ email: '', password: '' });
  const [error, setError] = useState('');

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    setError('');
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!form.email || !form.password) {
      setError('Please fill in all fields');
      return;
    }

    // Simulate login logic
    console.log('Logging in with:', form);
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">


      <form 
        onSubmit={handleSubmit}
        className="bg-white p-8 rounded-xl shadow-md w-full max-w-md animate__animated animate__backInUp"
      >
        <h2 className="text-2xl font-bold mb-6 text-gray-800">Login</h2>

        {error && (
          <div className="mb-4 text-red-500 text-sm">{error}</div>
        )}

        <div className="mb-4">
          <label className="block text-gray-700 mb-1" htmlFor="email">
            Email
          </label>
          <input
            type="email"
            name="email"
            value={form.email}
            onChange={handleChange}
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter your email"
          />
        </div>

        <div className="mb-6">
          <label className="block text-gray-700 mb-1" htmlFor="password">
            Password
          </label>
          <input
            type="password"
            name="password"
            value={form.password}
            onChange={handleChange}
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter your password"
          />
        </div>



     <p className='text-black py-3'> Dont have an account Go to  <span style={{color : "blue"}} onClick={()=>{setShowRegister(true)}}> Register  </span> </p>
        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-2 rounded-md hover:bg-blue-700 transition"
        >
          Login
        </button>
      </form>
    </div>
  );
};

export default LoginForm;
