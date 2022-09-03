using FitnessTracker.Contracts.Requests.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Contracts.Responses.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.WorkoutNames;

public interface IWorkoutNamesService
{
    public Task<Result<GetWorkoutNamesResponse>> GetWorkoutNames(int userId, GetWorkoutNamesRequest request);
}