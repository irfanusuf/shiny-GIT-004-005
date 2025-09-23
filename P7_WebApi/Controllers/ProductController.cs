using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P0_ClassLibrary.Interfaces;
using P7_WebApi.Data;
using P7_WebApi.Middlewares;
using P7_WebApi.Models.DomainModels;
using P7_WebApi.Models.JunctionModels;

namespace P7_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [Authorize]
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
            return Ok(new { message = "product found !", payload = product });
        }

        [HttpPost("addtocart")]

        public async Task<ActionResult> AddtoCart(Guid productId, int qty)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Items["userId"].ToString());

                var product = await sqlDb.Products.FindAsync(productId);
                if (product == null)
                {
                    return StatusCode(404, new { message = "Items not found !" });
                }
                // var user = await sqlDb.Users.Include(user => user.Cart).ThenInclude(cart => cart.CartProducts).FirstOrDefaultAsync(user => user.UserId == userId);
                var cart = await sqlDb.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(cart => cart.UserId == userId);


                if (cart == null)
                {
                    var newCart = new Cart
                    {
                        UserId = userId,
                        CartTotal = 0
                    };

                    var cartProduct = new CartProduct
                    {
                        CartId = newCart.CartId,
                        ProductId = productId,
                        Quantity = qty,
                        ProductPrice = product.ProductPrice
                    };

                    newCart.CartTotal = product.ProductPrice * qty;
                    await sqlDb.Carts.AddAsync(newCart);
                    await sqlDb.CartProducts.AddAsync(cartProduct);
                }
                else
                {

                    var existingCartProduct = await sqlDb.CartProducts
                    .FirstOrDefaultAsync(cp => cp.CartId == cart.CartId && cp.ProductId == productId);

                    if (existingCartProduct == null)
                    {
                        var cartProduct = new CartProduct
                        {
                            CartId = cart.CartId,
                            ProductId = productId,
                            Quantity = qty,
                            ProductPrice = product.ProductPrice
                        };

                        await sqlDb.CartProducts.AddAsync(cartProduct);
                    }
                    else
                    {

                        existingCartProduct.Quantity += qty;
                    }


                    cart.CartTotal += product.ProductPrice * qty;


                }

                await sqlDb.SaveChangesAsync();
                return Ok(new { messsage = "Product added to the cart succesfully !", payload = cart });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }



        [HttpPost("RemoveFromCart")]

        public async Task<ActionResult> RemoveFromCart(Guid productId)
        {
            try
            {  
                Guid? userId = HttpContext.Items["UserId"] as Guid?;
                var cart = await sqlDb.Carts
                .Include(cart => cart.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == userId);     // O(n)   // O(1)

                // var product = await sqlDb.Products.FindAsync(productId);   // O(1)

                if (cart == null)
                {
                    return NotFound(new { message = "Not Found ! Something Went Wrong ! " });
                }

                // cart products are already in buffer  it means no time complexity 
                var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.CartId == cart.CartId && cp.ProductId == productId);


                // o(n)
                // var cartProduct = await sqlDb.CartProducts
                // .FirstOrDefaultAsync(cp => cp.CartId == cart.CartId && cp.ProductId == productId);

                if (cartProduct == null)
                {
                    return NotFound(new { message = "Item not Found !" });
                }

                var remove = sqlDb.CartProducts.Remove(cartProduct);
                if (remove != null)
                {

                    cart.CartTotal -= cartProduct.Quantity * cartProduct.ProductPrice;
                    await sqlDb.SaveChangesAsync();
                }

                return Ok(new { message = "Cart item Removed !", payload = cart });

            }
            catch (System.Exception)
            {

                throw;
            }


        }



    }
}
