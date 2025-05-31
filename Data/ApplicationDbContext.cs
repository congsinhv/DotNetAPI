using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<DictionaryItem> DictionaryItems { get; set; } = null!;
    public DbSet<Workspace> Workspaces { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;

    // TODO: Should be removed
    // public DbSet<ForgotPassword> ForgotPasswords { get; set; } = null!;

    //public DbSet<Jwt> Jwts { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Proficiency> Proficiencies { get; set; } = null!;
    public DbSet<DetailChat> DetailChats { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Exam> Exams { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
