using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace P7_WebApi.Models.DomainModels;

public class Address
{
    [Key]
    public Guid AddressId { get; set; } = Guid.NewGuid(); //pk
    public required string Name { get; set; }
    public required string City { get; set; }
    public required string District { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
    public required string Pincode { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }


    
    public required Guid UserId { get; set; } // Fk 

    [ForeignKey("UserId")]

    [JsonIgnore]
    public User? Buyer { get; set; } // Navigation property


    [JsonIgnore]
    public ICollection<Order> Orders { get; set; } = [];// naviagtion property
}
