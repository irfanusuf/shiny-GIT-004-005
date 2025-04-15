using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Models.DomainModels;
using P4_WebMVC.Models.ViewModels;
using P4_WebMVC.Types;

namespace P4_WebMVC.Controllers
{
    public class NoticeController : Controller
    {
        // GET: NoticeContoller

        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;

        public NoticeController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {

                var notices = await dbContext.Notices.Where(x => x.IsPublished == true).ToListAsync();

                if (notices == null || notices.Count == 0)
                {
                    ViewBag.errorMessage = "No Latest notices found";

                }

             
                var viewModel = new NoticeViewModel
                {
                    Notices = notices
                };



                return View(viewModel);



            }
            catch (System.Exception ex)
            {
                ViewBag.errorMessage = "An error occurred while retrieving the notices.";
                ViewBag.ex = ex.Message;
                return View();
            }

        }


        [HttpGet]

        public async Task<IActionResult> CreateNotice()
        {

            var token = HttpContext.Request.Cookies["GradSchoolAuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "user");
            }

            var id = tokenService.VerifyTokenAndGetId(token);

            if (id == Guid.Empty)
            {
                return RedirectToAction("login", "user");
            }

            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return RedirectToAction("login", "User");
            }

            if (user?.Role == Role.Editor || user?.Role == Role.Admin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "user");
            }

        }


        [HttpPost]

        public async Task<ActionResult> CreateNotice(Notice notice)
        {
            // Validate the notice object
            if (ModelState.IsValid)
            {

                // Save the notice to the database
                await dbContext.Notices.AddAsync(notice);
                await dbContext.SaveChangesAsync();

                ViewBag.successMessage = "Notice Created Successfully";
                return View();
            }

            // If the model state is not valid, return the view with validation errors

            ViewBag.errorMessage = "Please fill all the fields with * on it ";
            return View(notice);
        }
    }
}
