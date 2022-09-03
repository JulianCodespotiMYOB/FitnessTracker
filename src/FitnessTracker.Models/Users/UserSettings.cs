using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Models.Users;

public class UserSettings
{
    public int Id { get; set; }
    public User User { get; set; }
    public WeightUnit WeightUnit { get; set; } = WeightUnit.Kilograms;
    public MeasurementUnit MeasurementUnit { get; set; } = MeasurementUnit.Metric;
    public bool DarkMode { get; set; } = false;
}