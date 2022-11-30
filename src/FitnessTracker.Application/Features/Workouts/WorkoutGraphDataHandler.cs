using FitnessTracker.Application.Common;
using FitnessTracker.Application.Features.Users;
using FitnessTracker.Contracts.Requests.WorkoutGraphData.Enums;
using FitnessTracker.Contracts.Requests.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Contracts.Responses.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Domain.Workouts;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.GraphData;
using FitnessTracker.Models.Users;
using Mapster;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Workouts;

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
                    WorkoutGraphType.Weight => WorkoutGraphDataCalculator.GetWeightGraphData(user, request.ExerciseName, request.Reps),
                    WorkoutGraphType.Distance => WorkoutGraphDataCalculator.GetDistanceGraphData(user, request.ExerciseName),
                    WorkoutGraphType.Reps => WorkoutGraphDataCalculator.GetRepsGraphData(user, request.ExerciseName),
                    WorkoutGraphType.Sets => WorkoutGraphDataCalculator.GetSetsGraphData(user, request.ExerciseName),
                    _ => new List<WorkoutGraphData>()
                },
                _ => "User not found")
            .Match(
                response => response.Any() ? Result<GetWorkoutGraphDataResponse>.Success(response.Adapt<GetWorkoutGraphDataResponse>()) : Result<GetWorkoutGraphDataResponse>.Failure("No data found"),
                error => Result<GetWorkoutGraphDataResponse>.Failure(error)
            ); 
}