using FitnessTracker.Contracts.Requests.WorkoutVolume;
using FitnessTracker.Contracts.Responses.WorkoutVolume;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.Activities;

public interface IActivityService
{
    public Task<Result<GetActivityVolumeResponse>> GetActivityVolume(GetActivityVolumeRequest request);
}