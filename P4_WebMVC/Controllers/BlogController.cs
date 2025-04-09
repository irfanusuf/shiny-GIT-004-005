
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Models.DomainModels;
using P4_WebMVC.Models.HybridModels;
using P4_WebMVC.Types;



namespace P4_WebMVC.Controllers
{
    public class BlogController : Controller
    {


        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;

        public BlogController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }




        public async Task<ActionResult> GetBlog(Guid BlogId)
        {
            try
            {
                var blog = await dbContext.Blogs.FirstOrDefaultAsync(b => b.BlogId == BlogId);
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

                if(user?.Role ==Role.Editor){
                        return View();
                }else{
                  return  RedirectToAction("blogs" , "home");
                }
         
        }


        [HttpPost]
        public async Task<ActionResult> CreateBlog(Blog model)
        {

            try
            {
                var token = HttpContext.Request.Cookies["GradSchoolAuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login", "User");
                }

                var id = tokenService.VerifyTokenAndGetId(token);

                var user = await dbContext.Users.FindAsync(id);

                if (string.IsNullOrEmpty(model.BlogTitle) ||
               string.IsNullOrEmpty(model.Description) ||
               string.IsNullOrEmpty(model.ShortDesc) ||
               string.IsNullOrEmpty(model.BlogImage))
                {
                    ViewBag.errorMessage = "All details are necessary";
                    return View();
                }

                // fetch token from cookies 
                // get id from token \
                // fetch user from db 


                model.Author = user;            // Efcore Automatic tracking   // equivalent code model.authorUserId = user.UserId
                model.DateCreated = DateTime.UtcNow;
                model.DateModified = DateTime.UtcNow;

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
