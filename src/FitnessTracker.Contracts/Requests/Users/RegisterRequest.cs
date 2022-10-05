using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Contracts.Requests.Users;

public class RegisterRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Email { get; set; } = default!;
    public decimal Height { get; set; } = default!;
    public decimal Weight { get; set; } = default!;
    public int Age { get; set; } = default!;
    public decimal? BenchPressMax { get; set; }
    public decimal? SquatMax { get; set; }
    public decimal? DeadliftMax { get; set; }
    public string BuddyName { get; set; } = default!;
    public WeightUnit WeightUnit { get; set; } = default!;
    public MeasurementUnit MeasurementUnit { get; set; } = default!;
    public Image? Avatar { get; set; }
}