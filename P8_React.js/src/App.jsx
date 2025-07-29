import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./styles/output.css";
import Navbar from "./components/shared/Navbar";
import Footer from "./components/shared/Footer";
import Home from "./components/pages/Home";
import About from "./components/pages/About";
import Services from "./components/pages/Services";
import { useState } from "react";

const App = () => {
  // jsx fragmentation


  const [username  , setUsername] = useState("tehleem")
  const [darkMode, setDarkMode] = useState(false);


  return (
    <>
      <BrowserRouter>
       {/* // passed username as a prop */}
        <Navbar username = {username} darkMode = {darkMode} setDarkMode = {setDarkMode}/>   


        <div className={darkMode ? "bg-neutral-900 h-150 text-white" : "bg-neutral-400 h-150"}>


          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/services" element={<Services />} />
          </Routes>
        </div>



        <Footer />
      </BrowserRouter>
    </>
  );
};

export default App;
