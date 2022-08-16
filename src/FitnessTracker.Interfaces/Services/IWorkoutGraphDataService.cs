using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services;

public interface IWorkoutGraphDataService
{
    public Task<Result<GetWorkoutGraphDataResponse>>
        GetWorkoutGraphData(GetWorkoutGraphDataRequest request, int userId);
}