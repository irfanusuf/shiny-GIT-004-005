using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using P7_WebApi.Models.JunctionModels;

namespace P7_WebApi.Models.DomainModels;

public class Cart
{
    [Key]
    public Guid CartId { get; set; } = Guid.NewGuid();

    public required Guid UserId { get; set; }  // Fk

    [ForeignKey("UserId")]


    [JsonIgnore]
    public User? Buyer { get; set; }  // navigation property //  belonging to a user


    public ICollection<CartProduct> CartProducts { get; set; } = []; //  collection of products in the cart

    // public ICollection<Product> Products { get; set; } = [];

    public required decimal CartTotal { get; set; } = 0; // total price of all products in the cart



}
