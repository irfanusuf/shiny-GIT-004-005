import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const LoginForm = ({ setShowRegister }) => {

  const [form, setForm] = useState({
    email: "",
    password: ""
  })

  const navigate = useNavigate()

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }


  const handleLogin = async (e) => {

    e.preventDefault()

    try {
      const url = "http://localhost:5095/api/User/Login"
      const res = await axios.post(url, form)

      if (res.status === 200) {
        toast.success(res.data.message)


        setTimeout(() => {
          navigate("/user/dashboard")
        }, 2000)
      }

    } catch (error) {
      const statCodesArr = [400, 401, 403, 404, 500]
      
      if (error.status) {
        if (statCodesArr.includes(error.status)) {
          toast.error(error.response.data.message)
        } else {
          toast.error("Some Network Error!")
        }
      }
    }

  }





  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">


      <form onSubmit={handleLogin}
        className="bg-white p-8 rounded-xl shadow-md w-full max-w-md animate__animated animate__backInUp"
      >
        <h2 className="text-2xl font-bold mb-6 text-gray-800">Login</h2>


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



        <p className='text-black py-3'> Dont have an account Go to  <span style={{ color: "blue" }} onClick={() => { setShowRegister(true) }}> Register  </span> </p>
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
