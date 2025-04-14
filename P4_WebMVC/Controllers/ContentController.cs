using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Types;

namespace P4_WebMVC.Controllers
{
    public class ContentController : Controller
    {
        // GET: ContentController

        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;

        public ContentController(ITokenService tokenService, SqlDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult> DashBoard()
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
       

    }
}
