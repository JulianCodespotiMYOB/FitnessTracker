using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.WorkoutVolume;
using FitnessTracker.Contracts.Responses.WorkoutVolume;
using FitnessTracker.Domain.Workouts;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.Activities;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Application.Features.Activities;

public class ActivityHandler : IActivityService
{
    private readonly IApplicationDbContext _applicationDbContext;
    
    public ActivityHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<GetActivityVolumeResponse>> GetActivityVolume(GetActivityVolumeRequest request)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(request.UserId, _applicationDbContext);
        if (user is null)
        {
            return Result<GetActivityVolumeResponse>.Failure("User not found");
        }

        Activity? activity = user.Workouts.SelectMany(w => w.Activities).FirstOrDefault(a => a.Id == request.ActivityId);
        if (activity is null)
        {
            return Result<GetActivityVolumeResponse>.Failure("Activity not found");
        }

        decimal activityVolume = ExerciseVolumeCalculator.CalculateVolumeForActivity(activity);
        
        GetActivityVolumeResponse activityVolumeResponse = new()
        {
            Volume = activityVolume
        };
        
        return Result<GetActivityVolumeResponse>.Success(activityVolumeResponse);
    }
}