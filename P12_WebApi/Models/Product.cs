
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P12_WebApi.Models.AbstractClass;

namespace P12_WebApi.Models
{
    public class Product : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Category { get; set; }  
        public required decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Stock { get; set; } = 0;
        public string? SKU { get; set; } 
        public string? Brand { get; set; }
        public List<string> Images { get; set; } = [];
        public double Rating { get; set; } = 0;
        public int ReviewsCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsOnSale { get; set; } = false;
        public bool IsFeatured { get; set; } = false;

 
    }
}
