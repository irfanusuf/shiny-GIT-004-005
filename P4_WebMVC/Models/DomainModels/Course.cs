using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4_WebMVC.Models.DomainModels;

public class Course
{

    public Guid CourseId { get; set; } = Guid.NewGuid();
    public required string CourseTitle { get; set; }
    public required string ThumbnailUrl { get; set; }
    public required string CourseDescription { get; set; }
    public required string CourseSyallbus { get; set; }
    public required string CourseDuration { get; set; }
    public required decimal CourseFees { get; set; }
    public required decimal Discount { get; set; }
    public Dictionary<string, string> VideoUrls { get; set; } = [];
    public Guid? TeacherUserId { get; set; }     // Fk 
    [ForeignKey("TeacherUserId")]
    public User? Teacher { get; set; }    // navigation Property 
    public ICollection<User> EnlistedStudents { get; set; } = [];
    public bool? IsUpdated { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;





}
