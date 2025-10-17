using System;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using P12_WebApi.Models;

namespace P12_WebApi.Services;

public class MongoDbService
{

    private readonly IMongoDatabase _database;    // inheritance 
                                                  // function which takes connection string from config and establishes a connection with mongodb server 

   // ctor
    public MongoDbService(IConfiguration configuration)
    {

        var connectionString = configuration["MongoDB:ConnectionString"];  // importing connection strting 

        var databaseName = configuration["MongoDB:DatabaseName"];     //importing data base name from appsettings

        var client = new MongoClient(connectionString);
        
        var database = client.GetDatabase(databaseName);

        _database = database;

    }


    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");



}



