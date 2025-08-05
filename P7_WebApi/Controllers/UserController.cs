using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Register(User req)
        {


            if (string.IsNullOrEmpty(req.Email) )
            {
                return StatusCode( 404, new{message = "All details Are required !"});
            }


            return Ok(new { message = "Register SuccessFull !" });

        }


    }
}
