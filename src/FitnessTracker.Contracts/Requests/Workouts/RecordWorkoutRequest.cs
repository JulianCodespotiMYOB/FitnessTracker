using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.Workouts;

public class RecordWorkoutRequest
{
    public Workout Workout { get; set; }
}