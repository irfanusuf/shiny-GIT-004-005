using System;
using System.ComponentModel.DataAnnotations;

namespace P4_WebMVC.Models.DomainModels;

public class User
{

[Key]
public required Guid UserId {get; set;} = Guid.NewGuid();
public required string Username {get; set;}
public required string Email {get; set;}
public required string Password {get; set;}
public required string Avatar {get; set;}


}
