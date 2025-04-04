using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Models.DomainModels;
using P4_WebMVC.Models.ViewModels;

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




        public ActionResult GetBlog()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreateBlog()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateBlog(Blog model)
        {

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
            var token = HttpContext.Request.Cookies["GradSchoolAuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }

            var id = tokenService.VerifyTokenAndGetId(token);

            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return RedirectToAction("register", "User");
            }

            model.Author = user;
            model.DateCreated =  DateTime.UtcNow;
            model.DateModified = DateTime.UtcNow;

            var createblog = await dbContext.Blogs.AddAsync(model);


            if(createblog == null){
                ViewBag.errorMessage = "some Error , try again after sometimr ";
                return View();

            }

            return RedirectToAction("blogs" , "home");
        }

    }
}
