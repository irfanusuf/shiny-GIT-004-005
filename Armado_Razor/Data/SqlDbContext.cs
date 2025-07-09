using System;
using Armado_Razor.Models;

using Microsoft.EntityFrameworkCore;

namespace Armado_Razor.Data;

public class SqlDbContext : DbContext
{


    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

    public DbSet<User> Users {get; set;}



}
