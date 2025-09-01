import { BrowserRouter, Route, Routes } from "react-router-dom";
import Navbar from "./components/shared/Navbar";
import Footer from "./components/shared/Footer";
import Home from "./components/pages/Home";
import About from "./components/pages/About";
import Services from "./components/pages/Services";
import Account from "./components/pages/Account";
import { ToastContainer } from "react-toastify";
import Pokemon from "./components/pages/Pokemon";




const App = () => {
  // jsx fragmentation

  return (
    <>

      <BrowserRouter>

        <ToastContainer />

        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/services" element={<Services />} />
          <Route path="/user/account" element={<Account />} />
          <Route path="/pokemon" element={<Pokemon/>}/>
        </Routes>
        <Footer />

      </BrowserRouter>
    </>
  );
};

export default App;
