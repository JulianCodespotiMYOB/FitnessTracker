using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Contracts.Responses.Workout;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces;

public interface IWorkoutService
{
    public Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request);
}