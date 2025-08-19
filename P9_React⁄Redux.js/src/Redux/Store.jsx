import { productReducer, userReducer } from "./Reducer";

const { configureStore } = require("@reduxjs/toolkit");





const store = configureStore({
    reducer: {

        user: userReducer,
        product : productReducer

    }
})





export default store