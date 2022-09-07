using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.Authorization;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using Mapster;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Users;

public class UserHandler : IAuthorizationService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public UserHandler(IApplicationDbContext applicationDbContext, ILogger<UserHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        Result<User> user = await UserHelper.GetUserFromDatabaseByEmail(request.Email, _applicationDbContext, _logger);
        if (user.IsSuccess is false)
        {
            return Result<LoginResponse>.Failure(user.Error);
        }

        if (!user.Value.Password.Equals(request.Password))
        {
            string message = $"Password for user with email {request.Email} is incorrect.";
            _logger.LogError(message);
            return Result<LoginResponse>.Failure(message);
        }

        LoginResponse response = new(user.Value);
        return Result<LoginResponse>.Success(response);
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        Result<User> user = await UserHelper.GetUserFromDatabaseByEmail(request.Email, _applicationDbContext, _logger);
        if (user.IsSuccess)
        {
            string message = $"User with email {request.Email} already exists.";
            _logger.LogError(message);
            return Result<RegisterResponse>.Failure(message);
        }

        User newUser = request.Adapt<User>();
        newUser.WorkoutBuddy = new WorkoutBuddy
        {
            Name = request.BuddyName
        };

        newUser.UserSettings = new UserSettings
        {
            MeasurementUnit = request.MeasurementUnit,
            WeightUnit = request.WeightUnit
        };

        await _applicationDbContext.Users.AddAsync(newUser);
        await _applicationDbContext.SaveChangesAsync();

        RegisterResponse response = new(newUser);
        return Result<RegisterResponse>.Success(response);
    }

    public async Task<Result<GetUserResponse>> GetUserAsync(int id)
    {
        Result<User> user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext, _logger);
        return user.IsSuccess
            ? Result<GetUserResponse>.Success(new GetUserResponse(user.Value))
            : Result<GetUserResponse>.Failure(user.Error);
    }

    public async Task<Result<GetUsersResponse>> GetUsersAsync()
    {
        Result<IEnumerable<User>> users = await UserHelper.GetUsersFromDatabase(_applicationDbContext, _logger);
        return users.IsSuccess
            ? Result<GetUsersResponse>.Success(new GetUsersResponse(users.Value))
            : Result<GetUsersResponse>.Failure(users.Error);
    }
}