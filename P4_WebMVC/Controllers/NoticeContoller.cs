using Microsoft.AspNetCore.Mvc;
using P4_WebMVC.Models.DomainModels;

namespace P4_WebMVC.Controllers
{
    public class NoticeController : Controller
    {
        // GET: NoticeContoller

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]

          public IActionResult CreateNotice()
        {
            return View();
        }


        [HttpPost]  

        public ActionResult CreateNotice(Notice notice)
        {
            // Validate the notice object
            if (ModelState.IsValid)
            {
                // Logic to create a notice
                // You can save the notice to the database or perform any other action here

                // Redirect to the Notices page after creating the notice
                return RedirectToAction("Notices");
            }

            // If the model state is not valid, return the view with validation errors
            return View(notice);
        }
    }
}
