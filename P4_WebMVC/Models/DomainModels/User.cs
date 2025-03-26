using System;
using System.ComponentModel.DataAnnotations;
using P4_WebMVC.Types;

namespace P4_WebMVC.Models.DomainModels;

public class User
{

[Key]
public  Guid UserId {get; set;} = Guid.NewGuid();
public required string Username {get; set;}
public required string Email {get; set;}
public required string Password {get; set;}
public required Role Role {get;set;} = Role.User;
public string ? Avatar {get; set;}


}
