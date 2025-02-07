using System;
using MongoDB.Bson;

namespace SqlDbConsole.Models;

public class User
{

public ObjectId Id {get ; set;}
public string Usename {get ; set;}
public string Email {get ; set;}
public string Password {get ; set;}


}
