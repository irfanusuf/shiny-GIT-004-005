
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using P12_WebApi.Models;
using P12_WebApi.Services;

namespace P12_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly MongoDbService db;

        public UserController(MongoDbService dbService )
        {
            db = dbService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> CreateUser(User req)
        {
            try
            {
                var user = await db.Users.Find(u => u.IsActive == true).ToListAsync();
                await db.Users.AsQueryable().FirstOrDefaultAsync(u => u.Email == req.Email);

                if (user == null)
                {
                   db.Users.InsertOne(req);
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
