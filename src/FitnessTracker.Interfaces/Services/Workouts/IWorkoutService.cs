using FitnessTracker.Contracts.Requests.Workouts.GetWorkouts;
using FitnessTracker.Contracts.Requests.Workouts.RecordWorkout;
using FitnessTracker.Contracts.Requests.Workouts.UpdateWorkout;
using FitnessTracker.Contracts.Requests.WorkoutVolume;
using FitnessTracker.Contracts.Responses.Workouts.DeleteWorkout;
using FitnessTracker.Contracts.Responses.Workouts.GetWorkouts;
using FitnessTracker.Contracts.Responses.Workouts.RecordWorkout;
using FitnessTracker.Contracts.Responses.Workouts.UpdateWorkout;
using FitnessTracker.Contracts.Responses.WorkoutVolume;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.Workouts;

public interface IWorkoutService
{
    public Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request, int userId);
    public Task<Result<GetWorkoutsResponse>> GetWorkouts(int userId, GetWorkoutsRequest request);
    public Task<Result<GetWorkoutResponse>> GetWorkout(int workoutId, int userId);
    public Task<Result<UpdateWorkoutResponse>> UpdateWorkout(UpdateWorkoutRequest request, int userId, int workoutId);
    public Task<Result<DeleteWorkoutResponse>> DeleteWorkout(int workoutId, int userId);
    public Task<Result<GetWorkoutVolumeResponse>> GetWorkoutVolume(GetWorkoutVolumeRequest request);
}