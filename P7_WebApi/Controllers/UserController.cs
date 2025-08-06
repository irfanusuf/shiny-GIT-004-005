using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7_WebApi.Data;
using P7_WebApi.Models;

namespace P7_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly SqlDbContext sqlDb;

        public UserController(SqlDbContext dbContext)
        {
            sqlDb = dbContext;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(User req)
        {
            if (string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
            {
                return StatusCode(404, new { message = "All details Are required !" });
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

            return Ok(new { message = "Register SuccessFull !"});

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
                return Ok(new { message = "Login SuccessFull !", payload = user });
            }
            else
            {
             return StatusCode(400, new { message = "Password incorrect!" });
            }


            

        }


    }
}
