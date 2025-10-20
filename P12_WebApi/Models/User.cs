using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using P12_WebApi.Models.AbstractClass;

namespace P12_WebApi.Models
{
    public class User : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Role { get; set; } = "customer";
        public bool IsActive { get; set; } = true;
        public List<Address> Addresses { get; set; } = [];

        //  summesion of individual cartItem's price * qty  
        public List<CartItem> Cart { get; set; } = [];    // this is our cart 
        public List<Order> Orders { get; set; } = [];
        public List<string> Wishlist { get; set; } = [];
        public DateTime? LastLogin { get; set; }
    
    }


}
