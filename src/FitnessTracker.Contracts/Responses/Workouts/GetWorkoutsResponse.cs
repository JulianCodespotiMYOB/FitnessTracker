using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Contracts.Responses.Workouts;

public class GetWorkoutsResponse
{
    public List<Workout> Workouts { get; set; }
}