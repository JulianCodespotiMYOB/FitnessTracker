using FitnessTracker.Models.Fitness.GraphData;

namespace FitnessTracker.Contracts.Responses.Workouts;

public class GetWorkoutGraphDataResponse
{
    public List<WorkoutGraphData> GraphData { get; set; }
}