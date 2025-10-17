using System;
using MongoDB.Bson.Serialization.Attributes;

namespace P12_WebApi.Models;

public class User
{

    // [BsonId]
    // public required string Id { get; set; }
    public required string Username { get; set; }

     public required string Email { get; set; }

     public required string Phone { get; set; }



}
