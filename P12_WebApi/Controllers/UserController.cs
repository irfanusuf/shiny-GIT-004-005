
using Microsoft.AspNetCore.Mvc;
using P12_WebApi.Models;
using P12_WebApi.Services;

namespace P12_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly MongoDbService mongoDB;

        public UserController(MongoDbService dbService)
        {
            mongoDB = dbService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> CreateUser(User user)
        {
            try
            {
                await mongoDB.Users.InsertOneAsync(user);
              return Ok();

                
            }
            catch (System.Exception)
            {
                
                throw;
            }
         
        }
    }
}
