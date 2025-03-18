using Microsoft.AspNetCore.Mvc;

namespace P4_WebMVC.Controllers
{
    public class HomeController : Controller
    {


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



    }
}
