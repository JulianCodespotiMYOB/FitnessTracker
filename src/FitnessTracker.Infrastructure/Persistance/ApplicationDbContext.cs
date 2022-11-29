using System.Reflection;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Models.Fitness.Datas;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<Workout> Workouts { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<Reward> Rewards { get; set; } = null!;
    public DbSet<Data> Data { get; set; } = null!;
    public DbSet<Activity> Activities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        optionsBuilder.EnableSensitiveDataLogging();
    }
}