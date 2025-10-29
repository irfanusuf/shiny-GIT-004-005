using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P10_WebApi.Models.AbstractClasses;


namespace P10_WebApi.Models;

public class Comment : BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CommentId { get; set; } 
    public required string PostId { get; set; }    
    public  required string UserId { get; set; }   
    public required string CommentText { get; set; }
    public bool FlaggedForReport { get; set; } = false;
    public bool IsEdited { get; set; } = false;
}
