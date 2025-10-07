using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using P7_WebApi.Models.JunctionModels;
using P7_WebApi.Types;

namespace P7_WebApi.Models.DomainModels;

public class Product
{

    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public required string ProductImage { get; set; }
    public required decimal ProductPrice { get; set; }
    public required int ProductStock { get; set; }
    public ProductCategory Category { get; set; } = ProductCategory.General;
    // seller data // in future 
    public string? Size { get; set; }
    public string? Color { get; set; }
    public string? Weight { get; set; }

    [JsonIgnore]
    public ICollection<CartProduct> ProductInCarts { get; set; } = [];   // navigation property //  collection of products in the cart
    [JsonIgnore]
    public ICollection<OrderProduct> ProductInOrders { get; set; } = []; //  collection of products in the order
    public bool IsAvailable { get; set; } = true;
    public bool IsArchived { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



}
