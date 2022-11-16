using FitnessTracker.Models.Fitness.Datas;
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
    DbSet<Image> Images { get; set; }
    DbSet<Reward> Rewards { get; set; }
    DbSet<Data> Data { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}