using FitnessTracker.Application.Authorization;
using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Contracts.Responses.Workout;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Workouts.Commands;

public class WorkoutHandler : IWorkoutService
{
    private readonly IApplicationDbContext applicationDbContext;
    private readonly ILogger logger;

    public WorkoutHandler(IApplicationDbContext applicationDbContext, ILogger<AuthorizationHandler> logger)
    {
        this.applicationDbContext = applicationDbContext;
        this.logger = logger;
    }

    public async Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request)
    {
        var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
        {
            logger.LogError($"User with id {request.UserId} not found");
            return Result<RecordWorkoutResponse>.Failure("User not found");
        }

        user.Workouts.Add(request.Workout);
        await applicationDbContext.SaveChangesAsync();

        var response = new RecordWorkoutResponse
        {
            Id = request.Workout.Id
        };

        return Result<RecordWorkoutResponse>.Success(response);
    }
}