using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4_WebMVC.Models.DomainModels;

public class Blog
{

    [Key]
    public Guid BlogId { get; set; } = Guid.NewGuid();
    public required string BlogImage { get; set; }
    public required string BlogTitle { get; set; }

    // relationship create   // here userid of author will be saved and that author userid will be foreign key which will be used to fetch user data from user table 

    public Guid AuthorId { get; set; } // this is the foreign key // this will be used to fetch the author details from user table

    [ForeignKey("AuthorId")]
    public required User Author { get; set; }   //navigation property  // and not the full details of author (user ) will be saved here // it is simply not possible in Sql 

    public required bool Publised {get;set;} = true;
    public required string ShortDesc { get; set; }
    public required string Description { get; set; }
    public required DateTime DateCreated { get; set; } 
    public required DateTime DateModified { get; set; }




}
