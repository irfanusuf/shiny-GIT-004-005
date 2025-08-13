import { userReducer } from "./Reducer";

const { configureStore } = require("@reduxjs/toolkit");





const store =   configureStore({
    reducer : {

        user : userReducer

    }
})





export default store