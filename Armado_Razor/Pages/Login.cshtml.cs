using Armado_Razor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Armado_Razor.Pages
{
    public class LoginModel : PageModel
    {


        private readonly SqlDbContext dbContext;


        public LoginModel(SqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnGet()
        {
        }


        public void OnPost()
        {
        }



    }
}
