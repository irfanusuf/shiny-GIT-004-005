import axios from 'axios';
import { useState } from 'react';


const RegisterForm = ({ setShowRegister }) => {



  const [error, setError] = useState('');
  const [message, setMessage] = useState("")


  const [username, setUsername] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')




  const handleRegister = async () => {

    try {
      const formData = { username, email, password }
      const url = "http://localhost:5095/api/User/Register"
      const response = await axios.post(url, formData)

      if (response.status === 200) {
        setMessage(response.data.message)
      }


    } catch (error) {
      console.error(error)
    }





  }





  return (


    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <form className="bg-white p-8 rounded-xl shadow-md w-full max-w-md animate__animated animate__backInDown" >
        <h2 className="text-2xl font-bold mb-6 text-gray-800">Register</h2>

        {error && (
          <div className="mb-4 text-red-500 text-sm">{error}</div>
        )}




        <div className="mb-4">
          <label className="block text-gray-700 mb-1" htmlFor="name">
            Username
          </label>

          <input
            type="text"
            value={username}
            onChange={(event) => { setUsername(event.target.value) }}
            className="w-full px-4 py-2 border  rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter your name"
          />
        </div>





        <div className="mb-4">
          <label className="block text-gray-700 mb-1" htmlFor="email">
            Email
          </label>
          <input
            type="email"
            value={email}
            onChange={(event) => { setEmail(event.target.value) }}
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter your email"
          />
        </div>




        <div className="mb-4">
          <label className="block text-gray-700 mb-1" htmlFor="password">
            Password
          </label>
          <input
            type="password"
            value={password}
            onChange={(event) => { setPassword(event.target.value) }}
            className="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter a password"
          />
        </div>




        <p className='text-black py-3'> already have an account go to <span style={{ color: "blue" }} onClick={() => { setShowRegister(false) }}> Login  </span> </p>



        <p>  {message} </p>

        <button
          onClick={handleRegister}
          type="button"
          className="w-full bg-green-600 text-white py-2 rounded-md hover:bg-green-700 transition"
        >


          Register
        </button>
      </form>
    </div>



  );
};

export default RegisterForm;
