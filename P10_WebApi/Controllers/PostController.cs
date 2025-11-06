using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using P0_ClassLibrary.Interfaces;
using P10_WebApi.Attributes;
using P10_WebApi.Models;
using P10_WebApi.Services;

namespace P10_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly MongoDbService db;
        private readonly ICloudinaryService cloudinaryService;

        public PostController(MongoDbService mongoDb, ICloudinaryService cloudinaryService)
        {
            db = mongoDb;
            this.cloudinaryService = cloudinaryService;

        }


        [Authroize]    // filter out unauthorized users 
        [HttpPost("create")]

        public async Task<ActionResult> Create([FromForm] Post req, IFormFile image)
        {
            string? userId = HttpContext.Items["userId"] as string;


            var secureUrl = cloudinaryService.UploadImageAsync(image, "P10WebApi");
            

            req.PostpicURL = secureUrl.Result;   // 

            req.UserId = userId;  // 


            await db.Posts.InsertOneAsync(req);    // 

            var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);

            var update = Builders<User>.Update.Push(u => u.Posts, req.PostId);

            await db.Users.UpdateOneAsync(filter, update);



            return Ok(new
            {
                message = "Post uploaded Suyccesfully!",
                payload = new
                {
                    postCaption = req.PostCaption,
                    secureUrl = secureUrl.Result
                }
            });
        }


        [Authroize]
        [HttpPost("like")]

        public async Task<ActionResult> LikePost(string postId)
        {
            // var post = await db.poe
            return Ok();
        }
    }
}
