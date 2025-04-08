using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Models.HybridModels;

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

                 var blogs =   await dbContext.Blogs.Where(b =>b.Publised == true ).ToListAsync();

                // Console.WriteLine( "Total blogs in the DB :"  +  blogs);

                var viewModel = new HybridViewModel
                {
                    Blogs = blogs
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



    }
}
