using FitnessTracker.Models.Fitness.Workout;

namespace FitnessTracker.Models.Authorization;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Workout> Workouts { get; set; } = new();
    public WorkoutBuddy.WorkoutBuddy WorkoutBuddy { get; set; } = new();
}