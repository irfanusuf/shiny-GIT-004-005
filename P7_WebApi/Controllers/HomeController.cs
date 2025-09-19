using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace P7_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok(new { message = "Server is Working" });
        }
    }
}
