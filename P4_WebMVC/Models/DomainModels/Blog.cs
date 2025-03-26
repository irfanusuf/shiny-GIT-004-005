using System;
using System.ComponentModel.DataAnnotations;

namespace P4_WebMVC.Models.DomainModels;

public class Blog
{

    [Key]
    public Guid BlogId { get; set; } = Guid.NewGuid();
    public required string BlogImage { get; set; }
    public required string BlogTitle { get; set; }

    // relationship create   // here userid of author will be saved and that author userid will be foreign key which will be used to fetch user data from user table 
    public required User Author { get; set; }    
    public required string ShortDesc { get; set; }
    public required string Description { get; set; }
    public required DateTime DateCreated { get; set; } 
    public required DateTime DateModified { get; set; }




}
