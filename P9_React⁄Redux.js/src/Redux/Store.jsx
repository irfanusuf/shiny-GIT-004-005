import { pokemonApi } from "./api/PokemonApi";
import { productReducer, userReducer } from "./Reducer";
import { fetchAllUsersSlice } from "./slices/userSlices";


const { configureStore } = require("@reduxjs/toolkit");





const store = configureStore({
    reducer: {

        user: userReducer,            // createReducer 
        product : productReducer,     // createReducer 
        usersSlice : fetchAllUsersSlice.reducer,    // createSlice
        [pokemonApi.reducerPath] : pokemonApi.reducer     // createApi 

    },
     middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(pokemonApi.middleware),
})





export default store