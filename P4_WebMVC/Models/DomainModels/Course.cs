using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4_WebMVC.Models.DomainModels;

public class Course
{

    public Guid CourseId { get; set; } = Guid.NewGuid();
    public required string CourseTitle { get; set; }
    public required string CourseDescription { get; set; }
    public required string CourseSyallbus { get; set; }
    public required string CourseDuration { get; set; }
    public required decimal CourseFees { get; set; }
    public required decimal Discount { get; set; }
    public string? VideoUrls { get; set; }   // e.g. "https://site.com/v1 ; https://site.com/v2"
    public string? ThumbnailUrl { get; set; }
    public Guid? TeacherUserId { get; set; }     // Fk 

    [ForeignKey("TeacherUserId")]
    public User? Teacher { get; set; }    // navigation Property 
    public ICollection<User> EnlistedStudents { get; set; } = [];
    public bool IsUpdated { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;



    [NotMapped]
    public List<string> VideoUrlList
    {
        get => string.IsNullOrWhiteSpace(VideoUrls)
            ? new List<string>()
            : VideoUrls.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

        set => VideoUrls = value == null ? null : string.Join(";", value);
    }

}
