using FitnessTracker.Application.Common;
using FitnessTracker.Application.Features.Authorization;
using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Requests.Workouts.Enums;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.WorkoutGraphData;

public class WorkoutGraphDataHandler : IWorkoutGraphDataService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public WorkoutGraphDataHandler(IApplicationDbContext applicationDbContext, ILogger<UserService> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }


    public async Task<Result<GetWorkoutGraphDataResponse>> GetWorkoutGraphData(GetWorkoutGraphDataRequest request,
        int userId)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext, _logger);
        if (userResult.IsSuccess is false) return Result<GetWorkoutGraphDataResponse>.Failure("User not found");

        User user = userResult.Value;

        Dictionary<int, double> dataToReturn = new();

        switch (request.WorkoutGraphType)
        {
            case WorkoutGraphType.Weight:
                dataToReturn = GetExerciseWeightDataAndTime(user, request.ExcerciseName, request.WeightUnit);
                break;
            case WorkoutGraphType.Distance:
                dataToReturn = GetExerciseDistanceDataAndTime(user, request.ExcerciseName);
                break;
            case WorkoutGraphType.Reps:
                dataToReturn = GetExerciseRepsDataAndTime(user, request.ExcerciseName);
                break;
            case WorkoutGraphType.Sets:
                dataToReturn = GetExerciseSetsDataAndTime(user, request.ExcerciseName);
                break;
            default:
                return Result<GetWorkoutGraphDataResponse>.Failure("Invalid workout graph type");
        }

        if (dataToReturn.Count == 0) return Result<GetWorkoutGraphDataResponse>.Failure("No data found");

        GetWorkoutGraphDataResponse response = new()
        {
            Data = dataToReturn
        };

        return Result<GetWorkoutGraphDataResponse>.Success(response);
    }

    private Dictionary<int, double> GetExerciseWeightDataAndTime(User user, string workoutName, WeightUnit weightUnit)
    {
        List<double> weightsForExercise = new();

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
                if (activity.Exercise.Name == workoutName)
                {
                    if (workout.WeightUnit == weightUnit) weightsForExercise.Add(activity.Data.Weight);
                    switch (workout.WeightUnit)
                    {
                        case WeightUnit.Kilograms when weightUnit == WeightUnit.Pounds:
                            weightsForExercise.Add(activity.Data.Weight * 2.20462);
                            break;
                        case WeightUnit.Pounds when weightUnit == WeightUnit.Kilograms:
                            weightsForExercise.Add(activity.Data.Weight / 2.20462);
                            break;
                    }
                }
        }

        Dictionary<int, double> weightsForExerciseAndTime = new();

        for (int i = 0; i < weightsForExercise.Count; i++) weightsForExerciseAndTime.Add(i, weightsForExercise[i]);

        return weightsForExerciseAndTime;
    }

    private Dictionary<int, double> GetExerciseDistanceDataAndTime(User user, string workoutName)
    {
        List<double> distancesForExercise = new();

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
                if (activity.Exercise.Name == workoutName)
                    distancesForExercise.Add(activity.Data.Distance);
        }

        Dictionary<int, double> distancesForExerciseAndTime = new();

        for (int i = 0; i < distancesForExercise.Count; i++)
            distancesForExerciseAndTime.Add(i, distancesForExercise[i]);

        return distancesForExerciseAndTime;
    }

    private Dictionary<int, double> GetExerciseRepsDataAndTime(User user, string workoutName)
    {
        List<double> repsForExercise = new();

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
                if (activity.Exercise.Name == workoutName)
                    repsForExercise.Add(activity.Data.Reps);
        }

        Dictionary<int, double> repsForExerciseAndTime = new();

        for (int i = 0; i < repsForExercise.Count; i++) repsForExerciseAndTime.Add(i, repsForExercise[i]);

        return repsForExerciseAndTime;
    }

    private Dictionary<int, double> GetExerciseSetsDataAndTime(User user, string workoutName)
    {
        List<double> setsForExercise = new();

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
                if (activity.Exercise.Name == workoutName)
                    setsForExercise.Add(activity.Data.Sets);
        }

        Dictionary<int, double> setsForExerciseAndTime = new();

        for (int i = 0; i < setsForExercise.Count; i++) setsForExerciseAndTime.Add(i, setsForExercise[i]);

        return setsForExerciseAndTime;
    }
}