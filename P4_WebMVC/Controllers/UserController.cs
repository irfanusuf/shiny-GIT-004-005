using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Models.DomainModels;

namespace P4_WebMVC.Controllers
{
    public class UserController : Controller
    {

        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;

        public UserController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.errorMessage = "All credentials Required";
                return View();
            }

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);   // db query 

            if (user != null)
            {
                ViewBag.errorMessage = "User Already exists";
                return View();
            }

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password); ;

            await dbContext.Users.AddAsync(model);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                ViewBag.errorMessage = "Email feild is required!";
                return View();
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.errorMessage = "Password Feild is Required!";
                return View();
            }


            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);   // db query 

            if (user == null)
            {
                ViewBag.errorMessage = "User Not Found!";
                return View();
            }

            var passVerify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (passVerify)
            {

                var token = tokenService.CreateToken(user.UserId, user.Email, user.Username, 60 * 24);

                HttpContext.Response.Cookies.Append(
                    "GradSchoolAuthToken",
                    token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddHours(24)
                    }
                );

                ViewBag.successMessage = "Logged in succesfully!";
                return View();
            }

            ViewBag.errorMessage = "Password incorrect!";
            return View();
        }
    }
}
