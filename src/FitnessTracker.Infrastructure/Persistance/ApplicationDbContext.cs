using System.Reflection;
using FitnessTracker.Interfaces;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.User;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IApplicationDbContext _context;

    public ApplicationDbContext()
    {
        _context = this;
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")!);
        optionsBuilder.EnableSensitiveDataLogging();
    }
}