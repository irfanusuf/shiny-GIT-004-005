

import React, { useState } from 'react'






const App = () => {



const [count , setCount] = useState(40) 




 function handleIncrement (){

    setCount(count => count +1)

}


 function handleDecrement  (){

    if(count>0){
   setCount(count => count -1)
    }
 

}
  return (
    <div><h1> bullets in the magazine {count} </h1>
    
        <button onClick={handleIncrement}> Increment </button>

         <button onClick={handleDecrement}> Fire </button>
    </div>
  )



}



export default App