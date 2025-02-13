
using Microsoft.AspNetCore.Mvc;
using WebApI.Models;
using WebApI.Interfaces;
using BCrypt.Net;

namespace WebApI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly ISqlService _sqlService;    // private feild 

        // primary constructor
        public UserController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            try
            {
                var existingUser = _sqlService.FindUser(user.Email);

                if (existingUser.Email == "")
                {
                    var encryptedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _sqlService.CreateUser(user.Username, user.Email, encryptedPass);
                    return Ok(new
                    {
                        message = "User Created Succesfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "User Already Exists!"
                    });
                }
            }
            catch (Exception error)
            {

                Console.WriteLine(error.Message);
                return StatusCode(500, new
                {
                    message = error.Message
                });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            try
            {
                var existingUser = _sqlService.FindUser(user.Email);

                if (existingUser.Email == "")
                {
                    return StatusCode(400, new
                    {
                        message = "No User Found"
                    });
                }
                else
                {
                    var checkPass = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

                    if (checkPass)
                    {
                        return StatusCode(200, new
                        {
                            message = "Logged In SuccesfullY!"
                        });

                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            message = "PassWord Incoreeect!"
                        });
                    }
                }
            }
            catch (Exception error)
            {

                return StatusCode(500, new
                {
                    message = error.Message
                });
            }
        }

       
        [HttpGet("Dashboard")]

        public void Dashboard(User user)
        {



        }


    }
}
