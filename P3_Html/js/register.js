document.getElementById("submit").addEventListener("click", async (event) => {
  event.preventDefault();
  console.log("Submit button is clicked ");

  let username = document.getElementById("username").value;
  let email = document.getElementById("email").value;
  let password = document.getElementById("password").value;

  // browser api

  try {
    const url = "http://localhost:5184/v1/user/register";

    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, email, password }),
    });

    const data = await response.json();

    if (data.success === true) {
      window.alert(data.message);
    } else {
      window.alert("Something went wrong!");
    }
  } catch (error) {
    // if(error.response )    // tommoreewww 
    console.log(error);
  }
});
