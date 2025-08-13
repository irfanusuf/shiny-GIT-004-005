import { createReducer } from "@reduxjs/toolkit";


const intialState ={
    user: {},
    orders: [],
    order: {},
    products: [],
    product: {},
    cart: {},
    addresses: [],
    address: {},
    darkMode: false,
    loading: false,
}


export const userReducer =    createReducer(intialState , (builder) =>{


    builder.addCase("TOGGLE_DARK_MODE"  , (state , action)=>{

        state.darkMode = true

    })


    builder.addCase("REQ_USER"    ,  (state , action)=>{

            state.user = action.payload

    })



} )