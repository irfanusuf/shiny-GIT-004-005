import React, { useEffect, useState } from "react";

const App = () => {
  const [count, setCount] = useState(40);
  const [explosionCounter, SetEXplosionCounter] = useState(10)

  function handleIncrement() {
    setCount((count) => count + 1);
  }

  function handleDecrement() {
    if (count > 0) {
      setCount((count) => count - 1);
    }
  }


  useEffect(() => {

    SetEXplosionCounter((explosionCounter) => explosionCounter - 1)

  } , [] )



  return (
    <div style={{ display: "flex", justifyContent: "center", flexDirection: "column", width: "50%", margin: "auto" }}>
      <h1> bullets in the magazine {count} </h1>



      <button onClick={handleIncrement}> Increment </button>

      <button onClick={handleDecrement}> Fire </button>



      <h1>

        Bomb planted :

        <p> Explosion in {explosionCounter} seconds  </p>

      </h1>
    </div>
  );
};

export default App;
