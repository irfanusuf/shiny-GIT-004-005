using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult> CreateProduct(Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.ProductName))
                {
                    return BadRequest(new { message = "product name is required !" });
                }

                await sqlDb.Products.AddAsync(product);
                await sqlDb.SaveChangesAsync();
                return Ok(new { message = "Product saved succesfully !" });
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [HttpGet("archive")]

        public async Task<ActionResult> ArchiveProduct(Guid productId)
        {
            try
            {
                var product = await sqlDb.Products.FindAsync(productId);

                if (product == null)
                {
                    return NotFound(new { message = "Product not found!", productId });
                }

                if (product.IsArchived == false && product.IsAvailable == true)
                {
                    product.IsArchived = true;
                    product.IsAvailable = false;
                }
                else
                {
                    return BadRequest(new { message = "Product is already in archived list!" });
                }

                await sqlDb.SaveChangesAsync();

                return Ok(new { message = "product archived Succesfully !" });
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [HttpGet("Unarchive")]

        public async Task<ActionResult> UnArchiveProduct(Guid productId)
        {
            try
            {
                var product = await sqlDb.Products.FindAsync(productId);

                if (product == null)
                {
                    return NotFound(new { message = "Product not found!", productId });
                }

                if (product.IsArchived == true && product.IsAvailable == false)
                {
                    product.IsArchived = false;
                    product.IsAvailable = true;
                }
                else
                {
                    return BadRequest(new { message = "Product is already in Unarchived list!" });
                }

                await sqlDb.SaveChangesAsync();

                return Ok(new { message = "product Unarchived Succesfully !" });
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [HttpPut("update")]


        public async Task<ActionResult> UpdateProduct(Guid productId, Product product)
        {

            try
            {

                var existingproduct = await sqlDb.Products.FindAsync(productId);

                if (existingproduct == null)
                {
                    return NotFound(new { message = "Product not found!", productId });
                }

                existingproduct.ProductName = product.ProductName;
                existingproduct.ProductDescription = product.ProductDescription;
                existingproduct.ProductImage = product.ProductImage;
                existingproduct.ProductStock = product.ProductStock;
                existingproduct.ProductPrice = product.ProductPrice;
                existingproduct.Size = product.Size;
                existingproduct.Color = product.Color;
                existingproduct.Weight = product.Weight;
                existingproduct.Category = product.Category;
                existingproduct.UpdatedAt = DateTime.Now;


                await sqlDb.SaveChangesAsync();

                return Ok(new { message = "Product updated successfully!", payload = existingproduct });

            }
            catch (System.Exception)
            {

                throw;
            }

        }




        [HttpGet("getAll")]


        public async Task<ActionResult> GetProducts()
        {


            var products = await sqlDb.Products.Where(p => p.IsAvailable == true).ToListAsync();


            return Ok(new { message = $"{products.Count} products found !", payload = products });


        }

        

        [HttpGet("getbyId")]
        public async Task<ActionResult> GetProductbyId(Guid productId)
        {
            var product = await sqlDb.Products.FindAsync(productId);
            return Ok(new {message = "product found !" , payload = product});
        }


    }
}
