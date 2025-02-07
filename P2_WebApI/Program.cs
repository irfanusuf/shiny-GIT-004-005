var builder = WebApplication.CreateBuilder(args);       // container

// conatiner ke ander service add / actiavte 

// dependency injection 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();     // msil


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//middle ware 
app.UseHttpsRedirection();



app.MapGet("/" , () => "Hello World from server ");

app.MapPost("/createUser" , (string username) => {
    

    if(username == "sayarMalik"){

        return Results.Ok(new{
            Message = $"Welcome {username} ",
            Login = true
        });

    } else{

        return Results.BadRequest(new{
            Message = $"Acces Denied",
            Login = false

        });
    }



});




app.MapPut("/" , () => "Hello World from server ");
app.MapDelete("/" , () => "Hello World from server ");





app.Run();

