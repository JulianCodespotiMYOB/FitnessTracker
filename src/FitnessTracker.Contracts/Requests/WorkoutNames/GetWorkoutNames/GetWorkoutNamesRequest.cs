using FitnessTracker.Contracts.Requests.WorkoutNames.Enums;

namespace FitnessTracker.Contracts.Requests.WorkoutNames.GetWorkoutNames;

public class GetWorkoutNamesRequest
{
    public int? Amount { get; set; }
    public WorkoutNamesOrder Order { get; set; }
}