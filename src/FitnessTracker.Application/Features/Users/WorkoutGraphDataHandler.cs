using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.WorkoutGraphData.Enums;
using FitnessTracker.Contracts.Requests.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Contracts.Responses.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Users;

public class WorkoutGraphDataHandler : IWorkoutGraphDataService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public WorkoutGraphDataHandler(IApplicationDbContext applicationDbContext, ILogger<UserHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<GetWorkoutGraphDataResponse>> GetWorkoutGraphData(GetWorkoutGraphDataRequest request, int userId) 
        => (await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext))
            .ToEither<User, string>()
            .Map(user => request.WorkoutGraphType switch
                {
                    WorkoutGraphType.Weight => GetWeightGraphData(user, request.ExerciseName, request.Reps),
                    WorkoutGraphType.Distance => GetDistanceGraphData(user, request.ExerciseName),
                    WorkoutGraphType.Reps => GetRepsGraphData(user, request.ExerciseName),
                    WorkoutGraphType.Sets => GetSetsGraphData(user, request.ExerciseName),
                    _ => new GetWorkoutGraphDataResponse()
                },
                _ => "User not found")
            .Match(
                response => response.GraphData.Any() ? Result<GetWorkoutGraphDataResponse>.Success(response) : Result<GetWorkoutGraphDataResponse>.Failure("No data found"),
                error => Result<GetWorkoutGraphDataResponse>.Failure(error)
            );

    private static GetWorkoutGraphDataResponse GetWeightGraphData(User user, string workoutName, int reps)
    {
        List<Models.Fitness.GraphData.WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts.Where(w => w.Completed))
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName && (activity.Data?.Reps ?? 0) == reps && activity.Data?.Weight != null)
                {
                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = activity.Data.Weight.Value,
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

        foreach (Workout workout in user.Workouts.Where(w => w.Completed)) 
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    decimal? distance = activity.Data.Distance;
                    if (distance == null) continue;
                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = distance.Value,
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

        foreach (Workout workout in user.Workouts.Where(w => w.Completed)) 
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    int? reps = activity.Data.Reps;
                    if (reps == null) continue;
                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = reps.Value,
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

        foreach (Workout workout in user.Workouts.Where(w => w.Completed)) 
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    int? sets = activity.Data.Sets;
                    if (sets == null) continue;
                    graphData.Add(new Models.Fitness.GraphData.WorkoutGraphData
                    {
                        ExerciseMetaData = sets.Value,
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