using FitnessTracker.Application.Common;
using FitnessTracker.Application.Features.Authorization;
using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Requests.Workouts.Enums;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.WorkoutNames;

public class WorkoutNamesHandler : IWorkoutNamesService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public WorkoutNamesHandler(IApplicationDbContext applicationDbContext, ILogger<UserService> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<GetWorkoutNamesResponse>> GetWorkoutNames(int userId, GetWorkoutNamesRequest request)
    {
        Result<User> userResult = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext, _logger);
        if (userResult.IsSuccess is false) return Result<GetWorkoutNamesResponse>.Failure("User not found");

        User user = userResult.Value;
        List<string> workoutNames = user.Workouts.Select(w => w.Name).ToList();

        workoutNames = request.Order switch
        {
            WorkoutNamesOrder.Descending => workoutNames.OrderByDescending(w => workoutNames.Count(wn => wn == w))
                .Distinct().ToList(),
            WorkoutNamesOrder.Ascending => workoutNames.OrderBy(w => workoutNames.Count(wn => wn == w)).Distinct()
                .ToList(),
            _ => workoutNames
        };

        if (request.Amount is not null) workoutNames = workoutNames.Take(request.Amount.Value).ToList();

        GetWorkoutNamesResponse response = new()
        {
            WorkoutNames = workoutNames
        };

        return Result<GetWorkoutNamesResponse>.Success(response);
    }
}