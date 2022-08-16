using FitnessTracker.Contracts.Requests.Workouts.Enums;

namespace FitnessTracker.Contracts.Requests.Workouts;

public class GetWorkoutNamesRequest
{
    public int? Amount { get; set; }
    public WorkoutNamesOrder Order { get; set; }
}