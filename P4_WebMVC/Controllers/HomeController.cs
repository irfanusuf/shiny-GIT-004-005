using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Models.ViewModels;

namespace P4_WebMVC.Controllers
{
    public class HomeController : Controller
    {


        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;

        public HomeController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Courses()
        {

            return View();
        }

        public IActionResult Events()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Blogs()
        {
            try
            {

                // var token = HttpContext.Request.Cookies["GradSchoolAuthToken"];

                // if (string.IsNullOrEmpty(token))
                // {
                //     return View();
                // }

                // var id = tokenService.VerifyTokenAndGetId(token);

                // var user = await dbContext.Users.FindAsync(id);   // db se fetch kerhay jhai logged in  user


                 var blogs =   await dbContext.Blogs.Where(b =>b.Publised == true ).ToListAsync();

                var viewModel = new HybridViewModel
                {
                    Blogs = blogs,
                    // User = user
                };


                return View();
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
