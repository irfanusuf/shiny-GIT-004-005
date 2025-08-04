import React, { createContext, useState } from 'react'
import App from '../App';



export const Context = createContext();

const Store = () => {


  const [SSOT, setSSOT] = useState({
    username: "tehleem",
    email : "email@example.com",
    address : {},
    darkMode: false,
    loading: false,
    courses : [] ,
    enrolledCourse : {}
  });

  function setDarkMode() {
    setSSOT((prevState) => ({...prevState, darkMode: !SSOT.darkMode }));   // ensure state of other varibles doesnot change 
  }



  return (
    <Context.Provider value={{...SSOT , setDarkMode}}>
        <App/>
    </Context.Provider>

  )
}

export default Store