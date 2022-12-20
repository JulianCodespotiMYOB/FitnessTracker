using System.ComponentModel.DataAnnotations.Schema;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Models.Users;

public class User
{
    public int Id { get; set; }
    public Image? Avatar { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int WeeklyWorkoutAmountGoal { get; set; } = 3;
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
    public List<Max> Maxes { get; set; } = new();
    public List<Workout> Workouts { get; set; } = new();
    public WorkoutBuddy WorkoutBuddy { get; set; } = null!;
    public UserSettings UserSettings { get; set; } = null!;
    public List<int> ClaimedAchievements { get; set; } = new();
    public List<Reward> Inventory { get; set; } = new();
    public Title? Title { get; set; }
    public Badge? Badge { get; set; }
}

public record Max(int Id, string Exercise, int Reps, decimal Weight)
{
    [NotMapped]
    public decimal EstimatedOneRepMax => Weight * (36 / (37 - Reps));
    [NotMapped]
    public decimal TrainingMax => EstimatedOneRepMax * 0.9m;
}