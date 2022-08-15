using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Models.Users;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int WeeklyWorkoutAmountGoal { get; set; } = 3;
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
    public List<Workout> Workouts { get; set; } = new();
    public WorkoutBuddy WorkoutBuddy { get; set; }
}