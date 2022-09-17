namespace FitnessTracker.Contracts.Responses.WorkoutGraphData.GetWorkoutGraphData;

public class GetWorkoutGraphDataResponse
{
    public List<Models.Fitness.GraphData.WorkoutGraphData> GraphData { get; set; } = new();
}