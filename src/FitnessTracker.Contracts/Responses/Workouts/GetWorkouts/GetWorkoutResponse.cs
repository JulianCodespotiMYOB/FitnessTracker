using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Responses.Workouts.GetWorkouts;

public class GetWorkoutResponse
{
    public Workout Workout { get; set; }
}