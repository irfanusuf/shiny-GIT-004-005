using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P10_WebApi.Models.AbstractClasses;

namespace P10_WebApi.Models;

public class User : BaseEntity
{

    [BsonId]
    public ObjectId? UserId { get; set; } 
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? ProfilePic { get; set; }
    public string? Bio { get; set; }
    public string? OTP { get; set; }
    public DateTime? OTPExpiry { get; set; }

  
    public List<ObjectId> Posts { get; set; } = [];   // array of postids

 
    public List<ObjectId> Comments { get; set; } = []; // array of commentIds
    
 
    public List<ObjectId> LikesGiven { get; set; } = [];  // array of postIds of those post which are liked by this user
        
}
