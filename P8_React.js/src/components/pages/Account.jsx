import React, { useState } from "react";
import RegisterForm from "../molecules/Register";
import LoginForm from "../molecules/Login";

const Account = ({ darkMode, setDarkMode }) => {
  const [showRegister, setShowRegister] = useState(true);

  return (
    <div>
      {showRegister ? (
        <RegisterForm
          setShowRegister={setShowRegister}
          darkMode={darkMode}
          setDarkMode={setDarkMode}
        />
      ) : (
        <LoginForm
          setShowRegister={setShowRegister}
          darkMode={darkMode}
          setDarkMode={setDarkMode}
        />
      )}
    </div>
  );
};

export default Account;
