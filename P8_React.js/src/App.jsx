import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./styles/output.css";
import Navbar from "./components/shared/Navbar";
import Footer from "./components/shared/Footer";
import Home from "./components/pages/Home";
import About from "./components/pages/About";
import Services from "./components/pages/Services";
import { createContext, useState } from "react";
import Account from "./components/pages/Account";
import 'animate.css';


export const Context = createContext()

const App = () => {
  // jsx fragmentation



  const [SSOT , setSSOT] = useState({
    username : "tehleem",
    darkMode : false ,
    loading : false
  })


  function setDarkMode (){
    setSSOT((prevState) => ({...prevState , darkMode : !SSOT.darkMode}))
  }

  return (
    <>
      <BrowserRouter>
 
        <Context.Provider value={{...SSOT , setDarkMode}}>

         <Navbar/>       
        <div className={SSOT.darkMode ? "bg-neutral-900  text-white" : "bg-neutral-400 "}>
          <Routes>
            <Route path="/" element={<Home/>}/>
            <Route path="/about" element={<About />} />
            <Route path="/services" element={<Services />} />
            <Route path="/user/account" element={<Account/>} />
          </Routes>
        </div>
        <Footer />

        </Context.Provider>


      </BrowserRouter>
    </>
  );
};

export default App;
