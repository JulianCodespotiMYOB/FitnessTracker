using FitnessTracker.Application.Common;
using FitnessTracker.Application.Features.Users;
using FitnessTracker.Contracts.Requests.WorkoutGraphData.Enums;
using FitnessTracker.Contracts.Requests.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Contracts.Responses.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.WorkoutGraphData;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.WorkoutGraphData;

public class WorkoutGraphDataHandler : IWorkoutGraphDataService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public WorkoutGraphDataHandler(IApplicationDbContext applicationDbContext, ILogger<UserHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }


    public async Task<Result<GetWorkoutGraphDataResponse>> GetWorkoutGraphData(GetWorkoutGraphDataRequest request,
        int userId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext);
        if (user is null)
        {
            return Result<GetWorkoutGraphDataResponse>.Failure("User not found");
        }

        GetWorkoutGraphDataResponse response;
        switch (request.WorkoutGraphType)
        {
            case WorkoutGraphType.Weight:
                response = GetWeightGraphData(user, request.ExerciseName, request.WeightUnit, request.Reps);
                break;
            case WorkoutGraphType.Distance:
                response = GetDistanceGraphData(user, request.ExerciseName);
                break;
            case WorkoutGraphType.Reps:
                response = GetRepsGraphData(user, request.ExerciseName);
                break;
            case WorkoutGraphType.Sets:
                response = GetSetsGraphData(user, request.ExerciseName);
                break;
            default:
                return Result<GetWorkoutGraphDataResponse>.Failure("Invalid workout graph type");
        }

        if (response.GraphData.Count == 0)
        {
            return Result<GetWorkoutGraphDataResponse>.Failure("No data found");
        }

        return Result<GetWorkoutGraphDataResponse>.Success(response);
    }

    private static GetWorkoutGraphDataResponse GetWeightGraphData(User user, string workoutName, WeightUnit weightUnit, int reps)
    {
        List<Models.Fitness.GraphData.WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName && activity.Data.Reps == reps)
                {
                    double weight = weightUnit switch
                    {
                        WeightUnit.Kilograms when weightUnit == WeightUnit.Pounds => activity.Data.Weight * 2.20462,
                        WeightUnit.Pounds when weightUnit == WeightUnit.Kilograms => activity.Data.Weight / 2.20462,
                        _ => activity.Data.Weight
                    };

                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = weight,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return new GetWorkoutGraphDataResponse
        {
            GraphData = graphData
        };
    }

    private static GetWorkoutGraphDataResponse GetDistanceGraphData(User user, string workoutName)
    {
        List<Models.Fitness.GraphData.WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = activity.Data.Distance,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return new GetWorkoutGraphDataResponse
        {
            GraphData = graphData
        };
    }

    private static GetWorkoutGraphDataResponse GetRepsGraphData(User user, string workoutName)
    {
        List<Models.Fitness.GraphData.WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = activity.Data.Reps,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return new GetWorkoutGraphDataResponse
        {
            GraphData = graphData
        };
    }

    private static GetWorkoutGraphDataResponse GetSetsGraphData(User user, string workoutName)
    {
        List<Models.Fitness.GraphData.WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts)
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = activity.Data.Sets,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return new GetWorkoutGraphDataResponse
        {
            GraphData = graphData
        };
    }
}