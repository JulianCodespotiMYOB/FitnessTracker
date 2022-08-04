using FitnessTracker.Application.Authorization;
using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Contracts.Responses.Workout;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Common;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Workouts;

public class WorkoutHandler : IWorkoutService
{
    private readonly IApplicationDbContext applicationDbContext;
    private readonly ILogger logger;

    public WorkoutHandler(IApplicationDbContext applicationDbContext, ILogger<UserHandler> logger)
    {
        this.applicationDbContext = applicationDbContext;
        this.logger = logger;
    }

    public async Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request, int userId)
    {
        var userResult = await UserHelper.GetUserFromDatabase(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<RecordWorkoutResponse>.Failure("User not found");
        var user = userResult.Value;
        user.Workouts.Add(request.Workout);
        await applicationDbContext.SaveChangesAsync();

        var response = new RecordWorkoutResponse
        {
            Id = request.Workout.Id
        };

        return Result<RecordWorkoutResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutsResponse>> GetWorkouts(int userId)
    {
        var userResult = await UserHelper.GetUserFromDatabase(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<GetWorkoutsResponse>.Failure("User not found");
        var user = userResult.Value;
        var workouts = user.Workouts;
        var response = new GetWorkoutsResponse
        {
            Workouts = workouts
        };

        return Result<GetWorkoutsResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutResponse>> GetWorkout(int workoutId, int userId)
    {
        var userResult = await UserHelper.GetUserFromDatabase(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<GetWorkoutResponse>.Failure("User not found");
        var user = userResult.Value;

        var workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<GetWorkoutResponse>.Failure("Workout not found");

        var response = new GetWorkoutResponse
        {
            Workout = workout
        };
        return Result<GetWorkoutResponse>.Success(response);
    }

    public async Task<Result<UpdateWorkoutResponse>> UpdateWorkout(UpdateWorkoutRequest request, int workoutId,
        int userId)
    {
        var userResult = await UserHelper.GetUserFromDatabase(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<UpdateWorkoutResponse>.Failure("User not found");
        var user = userResult.Value;

        var workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<UpdateWorkoutResponse>.Failure("Workout not found");

        user.Workouts.Remove(workout);

        request.Workout.Id = workoutId;
        user.Workouts.Add(request.Workout);
        await applicationDbContext.SaveChangesAsync();

        var response = new UpdateWorkoutResponse
        {
            Id = request.Workout.Id
        };

        return Result<UpdateWorkoutResponse>.Success(response);
    }

    public async Task<Result<DeleteWorkoutResponse>> DeleteWorkout(int workoutId, int userId)
    {
        var userResult = await UserHelper.GetUserFromDatabase(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<DeleteWorkoutResponse>.Failure("User not found");
        var user = userResult.Value;

        var workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<DeleteWorkoutResponse>.Failure("Workout not found");

        user.Workouts.Remove(workout);
        await applicationDbContext.SaveChangesAsync();

        var response = new DeleteWorkoutResponse
        {
            Id = workoutId
        };

        return Result<DeleteWorkoutResponse>.Success(response);
    }
}