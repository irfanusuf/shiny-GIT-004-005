using System;
using MongoDB.Bson.Serialization.Attributes;

namespace P12_WebApi.Models;

 public class Address
    {                                                                                                                                                                                                     
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string ZipCode { get; set; }
        public string Country { get; set; } = "India";
        public bool IsDefault { get; set; } = false;
    }
