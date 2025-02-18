
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
        private readonly ITokenService _tokenService;    // private feild 
        private readonly IMailService _mailService;

        // primary constructor
        public UserController(ISqlService sqlService, ITokenService tokenService, IMailService mailService)
        {
            _sqlService = sqlService;
            _tokenService = tokenService;
            _mailService = mailService;
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

                        var token = _tokenService.CreateToken(existingUser.Id.ToString(), existingUser.Email, existingUser.Username);

                        return StatusCode(200, new
                        {
                            message = "Logged In SuccesfullY!",
                            token
                        });

                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            message = "PassWord Incorrect!"
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


        [HttpDelete("Delete")]
        public IActionResult DeleteAccount(Login deleteUser)
        {
            try
            {
                var user = _sqlService.FindUser(deleteUser.Email);

                if (user.Email == "")
                {
                    return StatusCode(400, new
                    {
                        message = "No User Found"
                    });
                }
                var passVerify = BCrypt.Net.BCrypt.Verify(deleteUser.Password, user.Password);

                if (passVerify)
                {
                    var delete = _sqlService.DeleteUser(deleteUser.Email);

                    return StatusCode(200, new
                    {
                        message = "User Account Deleted succesfully!"
                    });
                }
                else
                {
                    return StatusCode(400, new
                    {
                        message = "Pass incorrect"
                    });
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


        [HttpPost("Forgot-pass")]
        public async Task<IActionResult> ForgotPass(string email)
        {
            try
            {
                var findUser = _sqlService.FindUser(email);

                if (findUser.Email == "")
                {
                    return StatusCode(400, new
                    {
                        message = "User Not Found!"
                    });
                }

                // this procedure will be offloaded to another thread and when completed (resolved) then return ok 
                // but the main thread will not remain blocked for other requests .....
                await _mailService.SendEmailAsync(email, "Forgot password Link ", "This is link ", true);

                return Ok(new
                {
                    link = "link is sent to your email"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "server Error"
                });
            }
        }



        [HttpGet("Dashboard")]

        public void Dashboard(User user)
        {



        }


    }
}
