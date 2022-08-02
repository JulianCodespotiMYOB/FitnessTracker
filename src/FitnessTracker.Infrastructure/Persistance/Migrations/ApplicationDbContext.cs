using System.Reflection;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Excercises.Data;
using FitnessTracker.Models.Excercises.Excercise;
using FitnessTracker.Models.Excercises.Workout;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Persistance.Migrations;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IApplicationDbContext context;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
            Workouts = new List<Workout>
            {
                new()
                {
                    Id = 1,
                    Time = 100,
                    Activities = new List<Activity>
                    {
                        new()
                        {
                            Id = 1,
                            Excercise = new Excercise
                            {
                                Description = "Bench Press",
                                Name = "Bench Press"
                            },
                            Data = new StrengthData
                            {
                                Reps = 10,
                                Sets = 3,
                                Weight = 100
                            }
                        }
                    }
                }
            }
        });
        context.SaveChangesAsync();
    }

    public DbSet<User> Users { get; set; }

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