using System;
using MongoDB.Driver;
using SqlDbConsole.Models;

namespace SqlDbConsole.Services;

public class Dummy
{

private readonly MongoService _mongoService;

public Dummy()
{
    MongoService mongoService = new();

_mongoService = mongoService;

}



public void CreateUser(User user){



mongoService.Users.InsertOne(user);

}


public void FindUser(string email){



mongoService.Users.Find(u => u.Email == email);;

}




}
