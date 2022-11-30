using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Requests.WorkoutVolume;

public class GetActivityVolumeRequest
{
    public int ActivityId { get; set; }
    public int UserId { get; set; }
}