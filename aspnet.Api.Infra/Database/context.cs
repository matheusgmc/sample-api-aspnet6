using Microsoft.EntityFrameworkCore;

using aspnet.Api.Core.Entities;

namespace aspnet.Api.Infra.Database;

public class DatabaseContext : DbContext
{
    public DbSet<UserEntity>? Users { get; set; }
    public DbSet<PostEntity>? Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<UserEntity>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = System.IO.Path.Join(
            Environment.CurrentDirectory,
            "..",
            "aspnet.Api.Infra",
            "Database",
            "db",
            "database.db"
        );
        Console.WriteLine($"{dbPath}");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        base.OnConfiguring(optionsBuilder);
    }
}
