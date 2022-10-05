using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Contracts.Requests.Users;

public class UpdateUserRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int WeeklyWorkoutAmountGoal { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
    public bool DarkMode { get; set; }
    public Image? Avatar { get; set; }
}