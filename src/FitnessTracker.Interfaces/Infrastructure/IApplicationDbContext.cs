using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Interfaces.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Exercise> Exercises { get; set; }
    DbSet<Workout> Workouts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}