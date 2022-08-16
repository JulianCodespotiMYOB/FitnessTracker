using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services;

public interface IWorkoutNamesService
{
    public Task<Result<GetWorkoutNamesResponse>> GetWorkoutNames(int userId, GetWorkoutNamesRequest request);
}