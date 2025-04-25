
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Models.DomainModels;
using P4_WebMVC.Models.ViewModels;
using P4_WebMVC.Types;



namespace P4_WebMVC.Controllers
{
    public class BlogController : Controller
    {


        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly ICloudinaryService cloudinaryService;

        public BlogController(SqlDbContext dbContext, ITokenService tokenService ,ICloudinaryService cloudinaryService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.cloudinaryService = cloudinaryService;
        }




        public async Task<ActionResult> GetBlog(Guid BlogId)
        {
            try
            {
                var blog = await dbContext.Blogs.Include(b => b.Author).FirstOrDefaultAsync(b => b.BlogId == BlogId);
                // var viewModel = new Blog{
                //     BlogTitle = blog.BlogTitle,
                //     BlogImage = blog.BlogImage,
                //     /// bs may thek gaya mughe code redundency pasand nahi hai 
                // };
                var viewModel = new HybridViewModel
                {
                    Blog = blog
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                ViewBag.errorMessage = "Something Went Wrong . Kindly try again after Some time";
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> CreateBlog()
        {

               var token = HttpContext.Request.Cookies["GradSchoolAuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login", "User");
                }

                var id = tokenService.VerifyTokenAndGetId(token);

                var user = await dbContext.Users.FindAsync(id);

                if(user?.Role ==Role.Editor || user?.Role == Role.Admin)   {
                        return View();
                }else{
                  return  RedirectToAction("Login" , "User");
                }
         
        }


        [HttpPost]
        public async Task<ActionResult> CreateBlog(Blog model , IFormFile file)
        {

            try
            {
                var token = HttpContext.Request.Cookies["GradSchoolAuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login", "User");
                }

                var id = tokenService.VerifyTokenAndGetId(token);

                // var user = await dbContext.Users.FindAsync(id);

                if (string.IsNullOrEmpty(model.BlogTitle) ||
               string.IsNullOrEmpty(model.Description) ||
               string.IsNullOrEmpty(model.ShortDesc))
            //    string.IsNullOrEmpty(model.BlogImage))
                {
                    ViewBag.errorMessage = "All details are necessary";
                    return View();
                }

                // fetch token from cookies 
                // get id from token \
                // fetch user from db 

                // upload image 
                var imageUrl =  await  cloudinaryService.UploadImageAsync(file);

                model.BlogImage = imageUrl;
                model.AuthorId= id;            // Efcore Automatic tracking   // equivalent code model.authorUserId = user.UserId
                

                await dbContext.Blogs.AddAsync(model);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("blogs", "home");
                

            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                ViewBag.errorMessage = "Something Went Wrong . Kindly try again after Some time";
                return View("Error");
            }


        }

    }
}
