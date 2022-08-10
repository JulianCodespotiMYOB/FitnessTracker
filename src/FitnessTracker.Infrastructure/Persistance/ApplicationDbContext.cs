using System.Reflection;
using AutoBogus;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Fitness.Workouts;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; } = null!;

    private readonly IApplicationDbContext context = null!;

    public ApplicationDbContext()
    {
        context = this;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasOne(u => u.WorkoutBuddy)
            .WithOne(wb => wb.User)
            .HasForeignKey<WorkoutBuddy>(wb => wb.Id);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        SaveChangesAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")!);
        optionsBuilder.EnableSensitiveDataLogging();
    }
}