using System;
using MongoDB.Bson.Serialization.Attributes;

namespace P12_WebApi.Models.AbstractClass;

public class BaseEntity
{

          [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}
