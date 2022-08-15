using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services;

public interface IWorkoutService
{
    public Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request, int userId);
    public Task<Result<GetWorkoutsResponse>> GetWorkouts(int userId, GetWorkoutsRequest request);
    public Task<Result<GetWorkoutResponse>> GetWorkout(int workoutId, int userId);
    public Task<Result<UpdateWorkoutResponse>> UpdateWorkout(UpdateWorkoutRequest request, int userId, int workoutId);
    public Task<Result<DeleteWorkoutResponse>> DeleteWorkout(int workoutId, int userId);
}