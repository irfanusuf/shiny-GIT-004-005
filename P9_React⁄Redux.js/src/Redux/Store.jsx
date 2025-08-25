import { productReducer, userReducer } from "./Reducer";
import fetchAllUsersSlice from "./actions/userSlices"

const { configureStore } = require("@reduxjs/toolkit");





const store = configureStore({
    reducer: {

        user: userReducer,
        product : productReducer,
        users : fetchAllUsersSlice

    }
})





export default store