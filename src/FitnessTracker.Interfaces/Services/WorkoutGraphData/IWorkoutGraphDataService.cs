using FitnessTracker.Contracts.Requests.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Contracts.Responses.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.WorkoutGraphData;

public interface IWorkoutGraphDataService
{
    public Task<Result<GetWorkoutGraphDataResponse>>
        GetWorkoutGraphData(GetWorkoutGraphDataRequest request, int userId);
}