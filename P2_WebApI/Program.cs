
using WebApI.Interfaces;
using WebApI.Services;



var builder = WebApplication.CreateBuilder(args);       // container

// conatiner ke ander service add / actiavte 

// dependency injection 


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors(Options => {Options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());});


builder.Services.AddSingleton<ISqlService , SqlService>();    // dependency injection 
builder.Services.AddSingleton<ITokenService , TokenService>();    // dependency injection 
builder.Services.AddSingleton<IMailService , EmailService>();    // dependency injection 


var app = builder.Build();     // msil


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//middle ware 
app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowAll");




app.Run();

