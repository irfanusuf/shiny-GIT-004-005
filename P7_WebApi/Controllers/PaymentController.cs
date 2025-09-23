using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace P7_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {


     [HttpGet("Pay")]
        public ActionResult Test(Guid orderId)
        {
            return Ok(new { message = "Server is Working" });
        }


    }
}
