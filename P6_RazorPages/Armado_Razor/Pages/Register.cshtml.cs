using System.Threading.Tasks;
using Armado_Razor.Data;
using Armado_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Armado_Razor.Pages
{
    public class RegisterModel : PageModel
    {

        // private readonly SqlDbContext dbContext;


        // public RegisterModel( SqlDbContext dbContext)
        // {
        //     this.dbContext = dbContext;
        // }


        public void OnGet()
        {
             Console.WriteLine("Some body is viewing register page"); 
        }

        public async Task OnPostAsync(User reqModel)
        {


            if (reqModel.Email == "")
            {
                // validations 
            }
            // var user = await dbContext.AddAsync(reqModel);
            // await dbContext.SaveChangesAsync();
            // post logic 

            Console.WriteLine(reqModel.Email);

        }


        public void OnPut()
        {

        }
        

        public void OnDelete()
        {
            
        }
        




    }

  
}



