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




}
