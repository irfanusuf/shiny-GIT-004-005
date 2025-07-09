
using Armado_Razor.Data;
using Armado_Razor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Armado_Razor.Pages
{
    public class RegisterModel(SqlDbContext dbContext) : PageModel
    {

        private readonly SqlDbContext dbContext = dbContext;

        public void OnGet()
        {
             
        }

        public async Task OnPostAsync(User reqModel)
        {
            if (!ModelState.IsValid)
            {
             TempData["ErrorMessage"] = "All params are required!";
            }



            // var user = await dbContext.AddAsync(reqModel);
            // await dbContext.SaveChangesAsync();
            // // post logic 

            // TempData["SuccessMessage"] = "User Created Succesfully!";

            // RedirectToPage("/login");

        }

    }

  
}



