using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P10_WebApi.Models;
using P10_WebApi.Services;

namespace P10_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly MongoDbService db;

        public PostController(MongoDbService mongoDb)
        {
            db = mongoDb;
        }



        [HttpPost("create")]

        public async Task<ActionResult> Create(Post req)
        {
            await db.Posts.InsertOneAsync(req);

            return Ok();
        }



    }
}
