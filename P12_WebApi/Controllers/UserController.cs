
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
        public async Task<ActionResult> CreateUser(User req)
        {
            try
            {
                var user = await mongoDB.Users.Find(u => u.Email == req.Email).FirstOrDefaultAsync();

                if (user == null)
                {
                    await mongoDB.Users.InsertOneAsync(req);
                    return StatusCode(201, new { message = "User Created Succesfully!", payload = req });
                }
                
                 
                return StatusCode(400 ,  new { message = "User Already Existed !" , payload = user }); 

            }
            catch (System.Exception)
            {
                
                throw;
            }
         
        }
    }
}
