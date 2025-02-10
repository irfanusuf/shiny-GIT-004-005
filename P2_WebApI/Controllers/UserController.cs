using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApI.Services;
using WebApI.Models;

namespace WebApI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly SqlService _sqlService;    // private feild 

        // primary conbstructore
        public UserController(SqlService sqlService)
        {
            _sqlService = sqlService;
        }


        [HttpPost("Register")]

        public IActionResult Register(User user)
        {

            try
            {
                // SqlService sqlService = new("sjhjs");    //instead of doing this 

                _sqlService.CreateUser(user.Username, user.Email, user.Password);

                return Ok();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }


        }


        [HttpPost("Login")]

        public void Login (User user){

    

        }

        [HttpGet("Dashboard")]

        public void Dashboard (User user){



        }


    }
}
