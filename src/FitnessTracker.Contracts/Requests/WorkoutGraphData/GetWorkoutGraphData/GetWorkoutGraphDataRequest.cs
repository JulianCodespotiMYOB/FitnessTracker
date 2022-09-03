using FitnessTracker.Contracts.Requests.WorkoutGraphData.Enums;
using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.WorkoutGraphData.GetWorkoutGraphData;

public class GetWorkoutGraphDataRequest
{
    public int AmountOfCoordinates { get; set; } = int.MaxValue;
    public WeightUnit WeightUnit { get; set; } = WeightUnit.Kilograms;
    public WorkoutGraphType WorkoutGraphType { get; set; }
    public string ExerciseName { get; set; }
    public int Reps { get; set; }
}