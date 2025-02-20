
using Microsoft.AspNetCore.Mvc;
using WebApI.Models;
using WebApI.Interfaces;
using BCrypt.Net;
using WebApI.Models.ViewModel;

namespace WebApI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISqlService sqlService;    // private feild 
        private readonly ITokenService tokenService;    // private feild 
        private readonly IMailService mailService;

        // primary constructor
        public UserController(ISqlService sqlService, ITokenService tokenService, IMailService mailService)
        {
            this.sqlService = sqlService;
            this.tokenService = tokenService;
            this.mailService = mailService;
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            try
            {
                var existingUser = sqlService.FindUser(user.Email);

                if (existingUser.Email == "")
                {
                    var encryptedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    // pass encryption updation \

                    user.UserId = Guid.NewGuid();
                    user.Password = encryptedPass;
                    sqlService.CreateUser(user);    // in upcoming days changes this method to async


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
                var existingUser = sqlService.FindUser(user.Email);

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

                        var token = tokenService.CreateToken(existingUser.UserId.ToString(), existingUser.Email, existingUser.Username, 60 * 24);

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
                var user = sqlService.FindUser(deleteUser.Email);

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
                    var delete = sqlService.DeleteUser(deleteUser.Email);

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
                var findUser = sqlService.FindUser(email);

                if (findUser.Email == "")
                {
                    return StatusCode(404, new
                    {
                        message = "User Not Found!"
                    });
                }


                var token = tokenService.CreateToken(findUser.UserId.ToString(), email, findUser.Username, 5);

                string link = $"https://www.algoacademy.in/forgotPass/{token}";
                // this procedure will be offloaded to another thread and when completed (resolved) then return ok 
                // but the main thread will not remain blocked for other requests .....
                await mailService.SendEmailAsync(email, "Forgot password Link ", $"Kindly click here to reset the Password {link}", false);

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


        [HttpPost("Change-password")]

        public async Task<IActionResult> ChangePassWord(string token , ChangePass updationReq )
        {

            try
            {

                var userId = tokenService.VerifyTokenAndGetId(token);

                var user =  sqlService.FindUser(userId);

                // user ka pass word changhe kerna hai 
                return Ok(new
                {   user, 
                    message = "Password updated Succesfullly"
                });



            }
            catch (Exception)
            {

                return StatusCode(500, new
                {
                    message = "Server Error"
                });
            }



        }

    }
}
