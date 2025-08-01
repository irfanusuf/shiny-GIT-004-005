import React, { useContext } from 'react'
import { Context } from '../../App'

const Testimonials = () => {



 const {username} = useContext(Context)



  return (
    <div>Testimonials by  {username} </div>
  )
}

export default Testimonials