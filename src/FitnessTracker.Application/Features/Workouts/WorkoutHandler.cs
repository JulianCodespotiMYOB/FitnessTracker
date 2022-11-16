using FitnessTracker.Application.Common;
using FitnessTracker.Application.Features.Users;
using FitnessTracker.Contracts.Requests.Workouts.GetWorkouts;
using FitnessTracker.Contracts.Requests.Workouts.RecordWorkout;
using FitnessTracker.Contracts.Requests.Workouts.UpdateWorkout;
using FitnessTracker.Contracts.Responses.Workouts.DeleteWorkout;
using FitnessTracker.Contracts.Responses.Workouts.GetWorkouts;
using FitnessTracker.Contracts.Responses.Workouts.RecordWorkout;
using FitnessTracker.Contracts.Responses.Workouts.UpdateWorkout;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.Workouts;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Workouts;

public class WorkoutHandler : IWorkoutService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public WorkoutHandler(IApplicationDbContext applicationDbContext, ILogger<UserHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<RecordWorkoutResponse>> RecordWorkout(RecordWorkoutRequest request, int userId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext);
        if (user is null)
        {
            _logger.LogWarning($"User with id {userId} not found");
            return Result<RecordWorkoutResponse>.Failure("User not found");
        }

        Result<List<Activity>> activities = MapExercises(request.Activities);
        if (!activities.IsSuccess)
        {
            _logger.LogWarning($"Failed to map exercises: {activities.Error}");
            return Result<RecordWorkoutResponse>.Failure(activities.Error);
        }

        Workout workout = new()
        {
            Activities = activities.Value,
            Completed = request.Completed,
            Time = request.Time,
            Past = request.Past,
            Name = request.Name,
        };

        user.Workouts.Add(workout);
        await _applicationDbContext.SaveChangesAsync();

        RecordWorkoutResponse response = new()
        {
            Id = workout.Id
        };

        return Result<RecordWorkoutResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutsResponse>> GetWorkouts(int userId, GetWorkoutsRequest request)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext);
        if (user is null)
        {
            _logger.LogError($"User with id {userId} not found");
            return Result<GetWorkoutsResponse>.Failure("User not found");
        }

        GetWorkoutsResponse response = new()
        {
            Workouts = user.Workouts
        };

        if (request.Name is not null)
        {
            response.Workouts = response.Workouts.Where(w => w.Name.Contains(request.Name)).ToList();
        }

        return Result<GetWorkoutsResponse>.Success(response);
    }

    public async Task<Result<GetWorkoutResponse>> GetWorkout(int workoutId, int userId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext);
        if (user is null)
        {
            _logger.LogError($"User with id {userId} not found");
            return Result<GetWorkoutResponse>.Failure("User not found");
        }

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null)
        {
            _logger.LogError($"Workout with id {workoutId} not found");
            return Result<GetWorkoutResponse>.Failure("Workout not found");
        }

        GetWorkoutResponse response = new()
        {
            Workout = workout
        };
        return Result<GetWorkoutResponse>.Success(response);
    }

    public async Task<Result<UpdateWorkoutResponse>> UpdateWorkout(UpdateWorkoutRequest request, int workoutId, int userId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext);
        if (user is null)
        {
            _logger.LogError($"User with id {userId} not found");
            return Result<UpdateWorkoutResponse>.Failure("User not found");
        }

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null)
        {
            _logger.LogError($"Workout with id {workoutId} not found");
            return Result<UpdateWorkoutResponse>.Failure("Workout not found");
        }

        Result<List<Activity>> activities = MapExercises(request.Workout.Activities);
        if (!activities.IsSuccess)
        {
            _logger.LogError($"Failed to map exercises");
            return Result<UpdateWorkoutResponse>.Failure(activities.Error);
        }

        workout.Past = request.Workout.Past;
        workout.Completed = request.Workout.Completed;
        workout.Time = request.Workout.Time;
        workout.Activities = activities.Value;
        workout.Name = request.Workout.Name;

        await _applicationDbContext.SaveChangesAsync();

        UpdateWorkoutResponse response = new()
        {
            Id = request.Workout.Id
        };

        return Result<UpdateWorkoutResponse>.Success(response);
    }

    public async Task<Result<DeleteWorkoutResponse>> DeleteWorkout(int workoutId, int userId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext);
        if (user is null)
        {
            _logger.LogError($"User with id {userId} not found");
            return Result<DeleteWorkoutResponse>.Failure("User not found");
        }

        Workout? workout = user.Workouts.FirstOrDefault(w => w.Id == workoutId);
        if (workout is null)
        {
            _logger.LogError($"Workout with id {workoutId} not found");
            return Result<DeleteWorkoutResponse>.Failure("Workout not found");
        }

        user.Workouts.Remove(workout);
        await _applicationDbContext.SaveChangesAsync();

        DeleteWorkoutResponse response = new()
        {
            Id = workoutId
        };

        return Result<DeleteWorkoutResponse>.Success(response);
    }

    private Result<List<Activity>> MapExercises(IEnumerable<Activity> activities)
    {
        List<Activity> mappedActivities = new();

        foreach (Activity activity in activities)
        {
            Exercise? exercise = _applicationDbContext.Exercises.Find(activity.Exercise.Id);
            if (exercise is null)
            {
                return Result<List<Activity>>.Failure("Exercise not found");
            }

            Image? existingImage = _applicationDbContext.Images.FirstOrDefault(i => i.Id == (activity.Data.Image == null ? -1 : activity.Data.Image.Id));
            activity.Data.Image = existingImage ?? activity.Data.Image;

            mappedActivities.Add(new Activity
            {
                Data = activity.Data,
                Exercise = exercise,
                Id = activity.Id,
            });
        }

        return Result<List<Activity>>.Success(mappedActivities);
    }
}