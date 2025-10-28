using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<GitHubProfileEntity> GitHubProfiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GitHubProfileEntity>()
            .HasIndex(p => p.Login)
            .IsUnique();
    }
}
