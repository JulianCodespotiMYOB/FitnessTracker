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
    public DbSet<User> Users { get; set; } = null!;

    private readonly IApplicationDbContext context = null!;

    public ApplicationDbContext()
    {
        Users.Add(new User
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

        SaveChangesAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("FitnessInMemoryDB");
        optionsBuilder.EnableSensitiveDataLogging();
    }
}