using Microsoft.EntityFrameworkCore;
using aspnet.Models;

public class DatabaseContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<PostModel> Posts { get; set; }

    public string DbPath { get; }

    public DatabaseContext()
    {
        DbPath = System.IO.Path.Join(Environment.CurrentDirectory, "Database", "database.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserModel>()
            .HasMany(b => b.posts)
            .WithOne(e => e.user)
            .HasForeignKey(e => e.user_id)
            .IsRequired();
    }
}
