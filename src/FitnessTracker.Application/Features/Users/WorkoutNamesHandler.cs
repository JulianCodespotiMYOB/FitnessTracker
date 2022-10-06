using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.WorkoutNames.Enums;
using FitnessTracker.Contracts.Requests.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Contracts.Responses.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Users;

public class WorkoutNamesHandler : IWorkoutNamesService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public WorkoutNamesHandler(IApplicationDbContext applicationDbContext, ILogger<UserHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<GetWorkoutNamesResponse>> GetWorkoutNames(int userId, GetWorkoutNamesRequest request)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(userId, _applicationDbContext);
        if (user is null)
        {
            return Result<GetWorkoutNamesResponse>.Failure("User not found");
        }

        List<string> workoutNames = user.Workouts.Select(w => w.Name).ToList();

        workoutNames = request.Order switch
        {
            WorkoutNamesOrder.Descending => workoutNames.OrderByDescending(w => workoutNames.Count(wn => wn == w))
                .Distinct().ToList(),
            WorkoutNamesOrder.Ascending => workoutNames.OrderBy(w => workoutNames.Count(wn => wn == w)).Distinct()
                .ToList(),
            _ => workoutNames
        };

        if (request.Amount is not null)
        {
            workoutNames = workoutNames.Take(request.Amount.Value).ToList();
        }

        GetWorkoutNamesResponse response = new()
        {
            WorkoutNames = workoutNames
        };

        return Result<GetWorkoutNamesResponse>.Success(response);
    }
}