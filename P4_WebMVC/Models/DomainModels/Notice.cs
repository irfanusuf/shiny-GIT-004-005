using System;
using System.ComponentModel.DataAnnotations;

namespace P4_WebMVC.Models.DomainModels;

public class Notice
{


    [Key]
    public Guid NoticeId { get; set; } = Guid.NewGuid();

    public required string Title { get; set; }
    public required string Description { get; set; }

    public  bool IsPublished { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddMinutes(330);


}
