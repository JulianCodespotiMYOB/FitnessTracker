using FitnessTracker.Contracts.Requests.Workouts.Enums;
using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.Workouts;

public class GetWorkoutGraphDataRequest
{
    public int AmountOfCoordinates { get; set; }
    public WeightUnit WeightUnit { get; set; } = WeightUnit.Kilograms;
    public WorkoutGraphType WorkoutGraphType { get; set; }
    public string ExcerciseName { get; set; }
}