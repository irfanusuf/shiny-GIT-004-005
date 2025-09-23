using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P7_WebApi.Middlewares;

namespace P7_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    [Authorize]
    public class CartController : ControllerBase
    {

        [HttpGet("IncreaseQty")]
        public ActionResult IncreaseQty(Guid cartProductId)
        {
            return Ok(new { message = "Server is Working" });
        }



        [HttpGet("DecreaseQty")]
        public ActionResult DecreaseQty(Guid cartProductId)
        {
            return Ok(new { message = "Server is Working" });
        }


        [HttpGet("ClearCart")]
        public ActionResult ClearCart(Guid cartId)
        {
            return Ok(new { message = "Server is Working" });
        }

    }
}
