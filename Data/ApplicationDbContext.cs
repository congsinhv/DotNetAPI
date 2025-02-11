using Microsoft.EntityFrameworkCore;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DictionaryItem> DictionaryItems { get; set; } = null!;
    public DbSet<Workspace> Workspaces { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
} 