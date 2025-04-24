
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Data;
using P4_WebMVC.Interfaces;
using P4_WebMVC.Services;
using WebApI.Services;
// container
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("main")));

// add services to the container 
builder.Services.AddControllersWithViews();

   // dependency injection 
builder.Services.AddSingleton<ITokenService , TokenService>();    // dependency injection 
builder.Services.AddSingleton<IMailService , EmailService>();  
builder.Services.AddSingleton<ICloudinaryService , CloudinaryService>(); 

var app = builder.Build();


if (app.Environment.IsProduction())
{
     app.UseHsts();
}



app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
