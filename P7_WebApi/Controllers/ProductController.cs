using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P7_WebApi.Data;
using P7_WebApi.Models.DomainModels;

namespace P7_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly SqlDbContext sqlDb;

        public ProductController(SqlDbContext dbContext)
        {
            sqlDb = dbContext;
        }



        [HttpPost("create")]
        public ActionResult CreateProduct(Product product)
        {
            try
            {

                return Ok(new {message = "Data reached to post man request object succesfully !"});
            }
            catch (System.Exception)
            {

                throw;
            }




        }




    }
}
