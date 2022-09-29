using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.Users;

public class UpdateSettingsRequest
{
    public WeightUnit WeightUnit { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
    public bool DarkMode { get; set; }
}