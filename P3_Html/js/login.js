
document.getElementById("loginForm").addEventListener("submit", async (event) => {
    event.preventDefault();
    console.log("Submit button is clicked ");
  
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;
  
    // browser api
  
    try {
      const url = "http://localhost:5184/v1/user/login";
  
      const response = await fetch(url, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });
  
      const data = await response.json();
  
      if (data.success === true) {
        localStorage.setItem("WebApiToken" , data.token)
        window.alert(data.message)
        window.location.href = "/pages/profile.html"
      } else {
        window.alert("Something went wrong!");
      }
    } catch (error) {
      // if(error.response )    // tommoreewww 
      console.log(error);
    }
  });
  