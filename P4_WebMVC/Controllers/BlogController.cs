using Microsoft.AspNetCore.Mvc;
using P4_WebMVC.Models.DomainModels;

namespace P4_WebMVC.Controllers
{
    public class BlogController : Controller
    {

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
          public ActionResult CreateBlog(Blog model)
        {
            return View();
        }

    }
}
