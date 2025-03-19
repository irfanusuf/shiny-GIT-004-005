using Microsoft.AspNetCore.Mvc;

namespace P4_WebMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
    }
}
