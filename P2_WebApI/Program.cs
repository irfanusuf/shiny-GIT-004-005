
using WebApI.Interfaces;
using WebApI.Services;


var builder = WebApplication.CreateBuilder(args);       // container

// conatiner ke ander service add / actiavte 

// dependency injection 


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<ISqlService , SqlService>();    // dependency injection 
builder.Services.AddSingleton<ITokenService , TokenService>();    // dependency injection 


// builder.Services.AddTransient<ISqlService , SqlService>();    // dependency injection 
// builder.Services.AddScoped<ISqlService , SqlService>();    // dependency injection 



var app = builder.Build();     // msil


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//middle ware 
app.UseHttpsRedirection();
app.MapControllers();





app.Run();

