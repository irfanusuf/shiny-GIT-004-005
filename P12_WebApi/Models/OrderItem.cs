using System;

namespace P12_WebApi.Models;

public class OrderItem
{


    public required string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
