
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P0_ClassLibrary.Interfaces;
using P7_WebApi.Data;
using P7_WebApi.Models.DomainModels;

namespace P7_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly SqlDbContext sqlDb;
        private readonly ITokenService tokenService;

        public UserController(SqlDbContext dbContext , ITokenService tokenService)
        {
            sqlDb = dbContext;
            this.tokenService = tokenService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(User req)
        {
            if (string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
            {
                return StatusCode(400, new { message = "All details Are required !" });
            }

            var user = await sqlDb.Users.FirstOrDefaultAsync(user => user.Email == req.Email);

            if (user != null)
            {
                return StatusCode(400, new { message = "User already exists" });
            }


            var encryptPass = BCrypt.Net.BCrypt.HashPassword(req.Password);

            req.Password = encryptPass;


            var newUser = await sqlDb.Users.AddAsync(req);

            await sqlDb.SaveChangesAsync();

            var token = tokenService.CreateToken(req.UserId , req.Email , req.Username, 60*24*7 );

                HttpContext.Response.Cookies.Append("P7WebApi_Auth_Token" , token , new CookieOptions
                {
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    HttpOnly = false,
                    Expires = DateTime.Now.AddDays(7)
                } );
            

            return Ok(new { message = "Register SuccessFull !", payload = newUser.Entity });

        }


        [HttpPost("Login")]
          public async Task<IActionResult> Login(User req)
        {
            if ( string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
            {
                return StatusCode(400, new { message = "All details Are required !" });
            }

            var user = await sqlDb.Users.FirstOrDefaultAsync(user => user.Email == req.Email);

            if (user == null)
            {
                return StatusCode(404, new { message = "User not Found !" });
            }

            var verify = BCrypt.Net.BCrypt.Verify(req.Password , user.Password);

            if (verify)
            {

                var token = tokenService.CreateToken(user.UserId , user.Email , user.Username ?? "John ", 60*24*7 );

                HttpContext.Response.Cookies.Append("P7WebApi_Auth_Token" , token , new CookieOptions
                {
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    HttpOnly = false,
                    Expires = DateTime.Now.AddDays(7)
                } );

                return Ok(new { message = "Login SuccessFull !", payload = user, token });
            }
            else
            {
             return StatusCode(400, new { message = "Password incorrect!" });
            }

        }
    }
}
