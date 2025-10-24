
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

        public UserController(MongoDbService dbService)
        {
            db = dbService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> CreateUser(User req)
        {

              var user = await db.Users
                                    .Find(u => u.Email == req.Email && u.IsActive == true)
                                    .FirstOrDefaultAsync();

            if (user == null)
            {
                db.Users.InsertOne(req);

                return StatusCode(201, new { message = "User Created Succesfully!", payload = req });
            }

            // throw new Exception("just for test");

            return StatusCode(400, new { message = "User Already Existed !", payload = user });


        }
    }
}
