using System;
using MongoDB.Bson;
using MongoDB.Driver;
using SqlDbConsole.Models;


namespace SqlDbConsole.Services;

public class MongoService
{
    private readonly IMongoDatabase _database;     // class ki feild hai    // I mongo interface import 

    public MongoService()
    {
        var client = new MongoClient("mongodb://localhost:27017");

        var database = client.GetDatabase("supremeWaffle");

        // untyped documents
        // var collection = database.GetCollection<BsonDocument>("users");

        //    var collection = database.GetCollection<User>("users");

        _database = database;

    }




    // oin this one liner method i m getting collection named users which is having blueprint of model User
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");





}
