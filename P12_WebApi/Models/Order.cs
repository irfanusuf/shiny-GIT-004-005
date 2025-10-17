
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P12_WebApi.Models.AbstractClass;

namespace P12_WebApi.Models;

public class Order :BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = [];
        public string? Description { get; set; }
        public string Status { get; set; } = "Pending"; 

    }