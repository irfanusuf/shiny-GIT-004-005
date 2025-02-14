using System;
using System.Text.Json.Serialization;

namespace WebApI.Models;

public class User
{

    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }



}
