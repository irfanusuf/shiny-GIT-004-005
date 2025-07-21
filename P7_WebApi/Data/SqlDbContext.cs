using System;
using Microsoft.EntityFrameworkCore;
using P7_WebApi.Models;

namespace P7_WebApi.Data;

public class SqlDbContext : DbContext
{


    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }



    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }



}
