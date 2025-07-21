using Armado_Razor.Data;
using Microsoft.EntityFrameworkCore;
using P0_ClassLibrary;
using P0_ClassLibrary.Models;
using P0_ClassLibrary.Interfaces;


var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<SqlDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("main")));

builder.Services.AddSingleton<ICalculatorService, CalculatorService>();   // striaght forward 


var cloudinaryurl = builder.Configuration["CloudinarySettings:CloudinaryUrl"] ?? throw new InvalidOperationException("cloudinary url config is missing!");


builder.Services.AddSingleton<ICloudinaryService>(options => new CloudinaryService(cloudinaryurl));





// regsiter the settings // hard way but necessary
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// register the service as we would have done very simple looking but overly abstarcted behnd the scenes 
builder.Services.AddSingleton<IMailService, EmailService>();




// builder.Services.AddSingleton<ITokenService , TokenService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
