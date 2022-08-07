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

<<<<<<< HEAD
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasOne(u => u.WorkoutBuddy)
            .WithOne(wb => wb.User)
            .HasForeignKey<WorkoutBuddy>(wb => wb.Id);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
=======
        SaveChangesAsync();
>>>>>>> 4bbf081f7934aed3b9edafe250c06a54fe041f66
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("FitnessInMemoryDB");
        optionsBuilder.EnableSensitiveDataLogging();
    }
}