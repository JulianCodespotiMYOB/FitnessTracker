using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Contracts.Responses.Workout;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces;

public interface IWorkoutService
{
    public Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request, int userId);
    public Task<Result<GetWorkoutsResponse>> GetWorkouts(int userId);
    public Task<Result<GetWorkoutResponse>> GetWorkout(int workoutId, int userId);
    public Task<Result<UpdateWorkoutResponse>> UpdateWorkout(UpdateWorkoutRequest request, int userId, int workoutId);
    public Task<Result<DeleteWorkoutResponse>> DeleteWorkout(int workoutId, int userId);

}