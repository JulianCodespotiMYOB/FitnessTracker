using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.WorkoutVolume;

public class GetWorkoutVolumeRequest
{
    public int WorkoutId { get; set; }
    public int UserId { get; set; }
}