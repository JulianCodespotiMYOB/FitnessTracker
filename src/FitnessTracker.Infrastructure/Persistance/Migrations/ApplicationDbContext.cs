using System.Reflection;
using FitnessTracker.Interfaces;
using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Persistance.Migrations;

public class ApplicationDbContext: DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    private readonly DbContextOptions<ApplicationDbContext> _options;
    private readonly IApplicationDbContext _context;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    {
        _options = options;
        _context = this;

        _context.Users.Add(new User(1, "JohnDoe", "abc123", "JohnDoe@localhost.com", "John", "Doe"));

        _context.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("FitnessDb");
    }
}