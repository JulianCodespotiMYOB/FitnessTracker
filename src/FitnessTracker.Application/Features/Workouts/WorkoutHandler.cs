using FitnessTracker.Application.Authorization;
using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Interfaces;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Workouts;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Workouts;

public class WorkoutHandler : IWorkoutService
{
    private readonly IApplicationDbContext applicationDbContext;
    private readonly ILogger logger;

    public WorkoutHandler(IApplicationDbContext applicationDbContext, ILogger<UserService> logger)
    {
        this.applicationDbContext = applicationDbContext;
        this.logger = logger;
    }

    public async Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request, int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, applicationDbContext, logger);

        if (userResult.IsSuccess is false) return Result<RecordWorkoutResponse>.Failure("User not found");

        User user = userResult.Value;
        user.Workouts.Add(request.Workout);
        await applicationDbContext.SaveChangesAsync();

        RecordWorkoutResponse response = new()
        {
            Id = request.Workout.Id
        };

        return Result<RecordWorkoutResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutsResponse>> GetWorkouts(int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false)
        {
            return Result<GetWorkoutsResponse>.Failure("User not found");
        }

        GetWorkoutsResponse response = new()
        {
            Workouts = userResult.Value.Workouts
        };
        return Result<GetWorkoutsResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutResponse>> GetWorkout(int workoutId, int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<GetWorkoutResponse>.Failure("User not found");
        User? user = userResult.Value;

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<GetWorkoutResponse>.Failure("Workout not found");

        GetWorkoutResponse response = new()
        {
            Workout = workout
        };
        return Result<GetWorkoutResponse>.Success(response);
    }

    public async Task<Result<UpdateWorkoutResponse>> UpdateWorkout(UpdateWorkoutRequest request, int workoutId,
        int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<UpdateWorkoutResponse>.Failure("User not found");
        User? user = userResult.Value;

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<UpdateWorkoutResponse>.Failure("Workout not found");

        user.Workouts.Remove(workout);

        request.Workout.Id = workoutId;
        user.Workouts.Add(request.Workout);
        await applicationDbContext.SaveChangesAsync();

        UpdateWorkoutResponse response = new()
        {
            Id = request.Workout.Id
        };

        return Result<UpdateWorkoutResponse>.Success(response);
    }

    public async Task<Result<DeleteWorkoutResponse>> DeleteWorkout(int workoutId, int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, applicationDbContext, logger);
        if (userResult.IsSuccess is false) return Result<DeleteWorkoutResponse>.Failure("User not found");
        User? user = userResult.Value;

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<DeleteWorkoutResponse>.Failure("Workout not found");

        user.Workouts.Remove(workout);
        await applicationDbContext.SaveChangesAsync();

        DeleteWorkoutResponse response = new()
        {
            Id = workoutId
        };

        return Result<DeleteWorkoutResponse>.Success(response);
    }
}