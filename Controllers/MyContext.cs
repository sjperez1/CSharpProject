#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace CSharpProject.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<User> User { get; set; }
}