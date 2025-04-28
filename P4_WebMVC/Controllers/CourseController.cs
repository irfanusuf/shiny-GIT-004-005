using Microsoft.AspNetCore.Mvc;
using P4_WebMVC.Models.DomainModels;

namespace P4_WebMVC.Controllers
{
    public class CourseController : Controller
    {
        // GET: CourseController
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
         public ActionResult Create()
        {


            // authorization logic 
            return View();
        }



        [HttpPost]
         public ActionResult Create(Course course)
        {
            return View();
        }



    }
}
