using System.Reflection;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Users;
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
    public DbSet<Exercise> Exercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=db.prulbxxcrnwticvwjnjw.supabase.co;Database=postgres;Username=postgres;Password=iLikeTrains100!");
        optionsBuilder.EnableSensitiveDataLogging();
    }
}