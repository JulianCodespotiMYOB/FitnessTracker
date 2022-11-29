using FitnessTracker.Models.Fitness.Datas;

namespace FitnessTracker.Contracts.Requests.Workouts.UpdateWorkout;

public record UpdateWorkoutRequest
{
    public Dictionary<int, Data> NewData { get; set; }
    public bool Completed { get; set; }
}