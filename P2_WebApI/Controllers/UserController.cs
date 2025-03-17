
using Microsoft.AspNetCore.Mvc;
using WebApI.Models;
using WebApI.Interfaces;
using WebApI.Models.ViewModel;

namespace WebApI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISqlService sqlService;    // private feild 
        private readonly ITokenService tokenService;    // private feild 
        private readonly IMailService mailService;    // private field

        // primary constructor
        public UserController(ISqlService sqlService, ITokenService tokenService, IMailService mailService)
        {
            this.sqlService = sqlService;
            this.tokenService = tokenService;
            this.mailService = mailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User model)
        {
            try
            {
                var existingUser = await sqlService.FindUser(model.Email);

                if (existingUser == null)
                {
                    var encryptedPass = BCrypt.Net.BCrypt.HashPassword(model.Password);

                    // pass encryption updation 

                    model.UserId = Guid.NewGuid();
                    model.Password = encryptedPass;
                    await sqlService.CreateUser(model);    


                    return Ok(new
                    {
                        success = true,
                        message = "User Created Succesfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "User Already Exists!"
                    });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Server Error"
                });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                var existingUser = await sqlService.FindUser(model.Email);

                if (existingUser == null)
                {
                    return StatusCode(400, new
                    {
                        success = false,
                        message = "No User Found"
                    });
                }
                else
                {
                    var checkPass = BCrypt.Net.BCrypt.Verify(model.Password, existingUser.Password);
                    if (checkPass)
                    {
                        var token = tokenService.CreateToken(existingUser.UserId, existingUser.Email, existingUser.Username, 60*24);
                        return StatusCode(200, new
                        {
                            success = true,
                            message = "Logged In SuccesfullY!",
                            token
                        });
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                             success = false,
                            message = "PassWord Incorrect!"
                        });
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Server Error"
                });
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAccount(Login model)
        {
            try
            {
                var user = await sqlService.FindUser(model.Email);

                if (user == null)
                {
                    return StatusCode(400, new
                    {
                        message = "No User Found"
                    });
                }
                var passVerify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                if (passVerify)
                {
                    var delete = await sqlService.DeleteUser(model.Email);

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
            catch (Exception)
            {

                return StatusCode(500, new
                {
                    message = "Server Error!"
                });
            }
        }

        [HttpPost("Forgot-pass")]
        public async Task<IActionResult> ForgotPass(Email model)
        {
            try
            {
                var findUser = await sqlService.FindUser(model.EMail);

                if (findUser == null)
                {
                    return StatusCode(404, new
                    {
                        message = "User Not Found!"
                    });
                }
                var token = tokenService.CreateToken(findUser.UserId, model.EMail, findUser.Username, 1);

                string link = $"https://www.robokids.netlify.app/pages/updatePassword.html?token={token}";
                // this procedure will be offloaded to another thread and when completed (resolved) then return ok 
                // but the main thread will not remain blocked for other requests .....
                await mailService.SendEmailAsync(model.EMail, "Forgot password Link ", $"Kindly click here to reset the Password {link}", false);

                return Ok(new
                {
                    success = true,
                    message = "Password reset link is sent to your email",
                    token
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new
                {
                    message = "server Error"
                });
            }
        }

        [HttpPost("Change-password")]
        public async Task<IActionResult> ChangePassWord(string token, ChangePass updationReq)
        {
            try
            {
                var userId = tokenService.VerifyTokenAndGetId(token);

                if (updationReq.Password != updationReq.ConfirmPassword)
                {
                    return StatusCode(400, new
                    {
                        message = "Password doesnot match"
                    });
                }

                var encryptedPass = BCrypt.Net.BCrypt.HashPassword(updationReq.Password);

                var updatePass = await sqlService.UpdatePass( userId, encryptedPass);

                if (updatePass)
                {
                    return Ok(new
                    {
                        message = "Password updated Succesfullly"
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        message = "Some Error during updating password!"
                    });
                }

            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("Token has expired"))
                {
                    return Unauthorized(new { message = "PassWord Reset Link Expired!" });
                }
                return StatusCode(500, new
                {
                    message = "Server Error"
                });
            }
        }
    }
}
