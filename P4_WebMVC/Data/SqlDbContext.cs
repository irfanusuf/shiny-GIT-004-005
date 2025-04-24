using System;
using Microsoft.EntityFrameworkCore;
using P4_WebMVC.Models.DomainModels;

namespace P4_WebMVC.Data;

public class SqlDbContext : DbContext
{

    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }


   // entities
    public DbSet<User> Users {get; set;}
    public DbSet<Blog> Blogs {get; set;}
    public DbSet<Notice> Notices {get; set;}


    // configure fleunt api model builder
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Blog>()
        .HasOne(b => b.Author)
        .WithMany(a =>a.Blogs)
        .HasForeignKey(b => b.AuthorId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete    


        modelBuilder.Entity<Course>()
        .HasOne(c => c.Teacher)
        .WithMany(a => a.Courses)
        .HasForeignKey( c => c.TeacherUserId)
        .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Course>().HasMany(c => c.EnlistedStudents);



    }




}
