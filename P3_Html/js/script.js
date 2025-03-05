// var x = 3;
// let y = 4;

// let myArr = ["laveeza", "muneeza", "mehran", "sayar", "hanan"];

// console.log(myArr.length);

// myArr.push("abid");

// function addition(n1, n2) {
//   console.log(n1 + n2);
// }

// if(x = 3){
//     console.log("condition is true ")
// }

// addition(3,5)

// console.log(myArr);

// document.getElementById("Submit").addEventListener("click", async (event) => {
//   try {
//     event.preventDefault();

//     const username = document.getElementById("Username").value;
//     const email = document.getElementById("Email").value;
//     const password = document.getElementById("Email").value;

//     const formBody = { username, email, password };

//     const response = await fetch("http://localhost:5080/api/user/regsiter", {
//       method: "POST",
//       body: formBody,
//     });

//     const data = await response.json();

//     window.alert(data.message)

//   } catch (error) {
//     console.log(error);
//   }
// });




document.getElementById("icon").addEventListener("click" , ()=>{


const navlinks = document.getElementById("nav-links")

navlinks.classList.toggle("link-toggle")

})