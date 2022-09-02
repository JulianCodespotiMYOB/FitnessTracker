using FitnessTracker.Contracts.Requests.Workouts.Enums;
using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.Workouts;

public class GetWorkoutGraphDataRequest
{
    public int AmountOfCoordinates { get; set; } = int.MaxValue;
    public WeightUnit WeightUnit { get; set; } = WeightUnit.Kilograms;
    public WorkoutGraphType WorkoutGraphType { get; set; }
    public string ExerciseName { get; set; }
    public int Reps { get; set; }
}