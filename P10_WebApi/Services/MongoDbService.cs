
using MongoDB.Driver;
using P10_WebApi.Models;

namespace P10_WebApi.Services;

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

        var database = client.GetDatabase(databaseName);   // either get the database or create 

        _database = database;

    }


    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");   

    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");

    public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");



}



