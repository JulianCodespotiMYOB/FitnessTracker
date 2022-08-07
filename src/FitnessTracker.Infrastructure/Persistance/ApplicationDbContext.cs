using System.Reflection;
using AutoBogus;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.WorkoutBuddy;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IApplicationDbContext context;
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext()
    {
        context = this;

        context.Users.Add(new User
        {
            Email = "JohnDoe@gmail.com",
            Username = "johndoe",
            Password = "123456",
            FirstName = "John",
            LastName = "Doe",
            Id = 1,
            Workouts = new AutoFaker<Workout>().Generate(3),
            WorkoutBuddy = new AutoFaker<WorkoutBuddy>()
        });
        context.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("FitnessDb");
    }
}