using Microsoft.EntityFrameworkCore;
using P0_ClassLibrary;
using P0_ClassLibrary.Interfaces;
using P0_ClassLibrary.Models;
using P7_WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(Options => {Options.AddPolicy("AllowFrontend", policy => 
policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());});



// register or configure the  options needed by EmailService having type of EmailSettings present in P0 classlibarary   
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var cloudinaryUri = builder.Configuration["Cloudinary:URI"] ?? throw new InvalidOperationException("Cloudinary url not set !");
var SecretKey = builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException(" Secret Key not set !");



// not regular you have to use lambda expression 
builder.Services.AddSingleton<ICloudinaryService>(_ => new CloudinaryService(cloudinaryUri));
builder.Services.AddSingleton<ITokenService>(_ => new TokenService(SecretKey));

// regular as usual 
builder.Services.AddSingleton<ICalculatorService, CalculatorService>();
// regular as usual   // but complexity behind
builder.Services.AddSingleton<IMailService, EmailService>();


builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cloud")));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.MapControllers();



app.Run();


