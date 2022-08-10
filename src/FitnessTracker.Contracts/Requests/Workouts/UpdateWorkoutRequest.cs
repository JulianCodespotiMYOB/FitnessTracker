using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.Workouts;

public class UpdateWorkoutRequest
{
    public Workout Workout { get; set; }
}