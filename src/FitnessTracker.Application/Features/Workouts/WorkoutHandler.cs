using FitnessTracker.Application.Common;
using FitnessTracker.Application.Features.Authorization;
using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Workouts;

public class WorkoutHandler : IWorkoutService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public WorkoutHandler(IApplicationDbContext applicationDbContext, ILogger<UserService> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request, int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext, _logger);

        if (userResult.IsSuccess is false) return Result<RecordWorkoutResponse>.Failure("User not found");

        User user = userResult.Value;
        user.Workouts.Add(request.Workout);
        await _applicationDbContext.SaveChangesAsync();

        RecordWorkoutResponse response = new()
        {
            Id = request.Workout.Id
        };

        return Result<RecordWorkoutResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutsResponse>> GetWorkouts(int userId, GetWorkoutsRequest request)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext, _logger);
        if (userResult.IsSuccess is false) return Result<GetWorkoutsResponse>.Failure("User not found");

        GetWorkoutsResponse response = new()
        {
            Workouts = userResult.Value.Workouts
        };

        if (request.Name is not null)
            response.Workouts = response.Workouts.Where(w => w.Name.Contains(request.Name)).ToList();

        return Result<GetWorkoutsResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutResponse>> GetWorkout(int workoutId, int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext, _logger);
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
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext, _logger);
        if (userResult.IsSuccess is false) return Result<UpdateWorkoutResponse>.Failure("User not found");

        User? user = userResult.Value;

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<UpdateWorkoutResponse>.Failure("Workout not found");

        UpdateWorkoutResponse response = new()
        {
            Id = request.Workout.Id
        };

        return Result<UpdateWorkoutResponse>.Success(response);
    }

    public async Task<Result<DeleteWorkoutResponse>> DeleteWorkout(int workoutId, int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext, _logger);
        if (userResult.IsSuccess is false) return Result<DeleteWorkoutResponse>.Failure("User not found");

        User? user = userResult.Value;

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null) return Result<DeleteWorkoutResponse>.Failure("Workout not found");

        user.Workouts.Remove(workout);
        await _applicationDbContext.SaveChangesAsync();

        DeleteWorkoutResponse response = new()
        {
            Id = workoutId
        };

        return Result<DeleteWorkoutResponse>.Success(response);
    }
}