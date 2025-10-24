using P0_ClassLibrary;
using P0_ClassLibrary.Interfaces;
using P0_ClassLibrary.Models;
using P12_WebApi.Middlewares;
using P12_WebApi.Services;
using Serilog;



// default logger disables 

// activates serilog which writes the logs in console and log.txt file 
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();



// container
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();


// Replace default logging
builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("AllowFrontend", policy =>
policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

// register or configure the  options needed by EmailService having type of EmailSettings present in P0 classlibarary   
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var cloudinaryUri = builder.Configuration["Cloudinary:URI"] ?? throw new InvalidOperationException("Cloudinary url not set !");
var SecretKey = builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException(" Secret Key not set !");



// not regular you have to use lambda expression 
builder.Services.AddTransient<ICloudinaryService>(_ => new CloudinaryService(cloudinaryUri));
builder.Services.AddTransient<ITokenService>(_ => new TokenService(SecretKey));

// regular as usual   // but complexity behind
builder.Services.AddSingleton<IMailService, EmailService>();

builder.Services.AddScoped<MongoDbService>();

builder.Services.AddMemoryCache();





var app = builder.Build();   //  ioc

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// middlewares  pipeline 

// app.UseSession();


app.UseMiddleware<CustomRateLimiter>();


app.UseMiddleware<ErrorHandler>();


app.UseHttpsRedirection();   // http ko https transfer 





// app.UseAuthentication();

app.UseCors("AllowFrontend");


app.MapControllers();

app.Run();

