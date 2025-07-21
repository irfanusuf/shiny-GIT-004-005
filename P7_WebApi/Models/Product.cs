using System;

namespace P7_WebApi.Models;

public class Product
{


    public Guid ProductId { get; set; } = Guid.NewGuid();
 public required string  Name { get; set; }

}
