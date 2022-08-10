using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Responses.Workouts;

public class GetWorkoutResponse
{
    public Workout Workout { get; set; }
}