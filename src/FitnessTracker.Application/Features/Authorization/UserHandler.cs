using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Common;
using Mapster;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Authorization;

public class UserService : IAuthorizationService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public UserService(IApplicationDbContext applicationDbContext, ILogger<UserService> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginParameters loginParameters)
    {
        Result<User> user = await UserHelper.GetUserFromDatabaseByEmail(loginParameters.Email, _applicationDbContext, _logger);

        if (user.IsSuccess is false)
        {
            return Result<LoginResponse>.Failure(user.Error);
        }

        if (!user.Value.Password.Equals(loginParameters.Password))
        {
            string message = $"Password for user with email {loginParameters.Email} is incorrect.";
            _logger.LogError(message);
            return Result<LoginResponse>.Failure(message);
        }

        LoginResponse response = new(user.Value);
        return Result<LoginResponse>.Success(response);
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegistrationParameters registrationParameters)
    {
        Result<User> user =
            await UserHelper.GetUserFromDatabaseByEmail(registrationParameters.Email, _applicationDbContext, _logger);
        if (user.IsSuccess)
        {
            return Result<RegisterResponse>.Failure(user.Error);
        }

        User newUser = registrationParameters.Adapt<User>();
        newUser.WorkoutBuddy = new WorkoutBuddy
        {
            Name = registrationParameters.BuddyName,
            Description = registrationParameters.BuddyDescription,
            IconId = registrationParameters.BuddyIconId
        };

        await _applicationDbContext.Users.AddAsync(newUser);
        await _applicationDbContext.SaveChangesAsync();

        RegisterResponse response = new(newUser);
        return Result<RegisterResponse>.Success(response);
    }

    public async Task<Result<User>> GetUserAsync(int id)
    {
        Result<User> user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext, _logger);
        return user.IsSuccess ? Result<User>.Success(user.Value) : Result<User>.Failure(user.Error);
    }

    public async Task<Result<IEnumerable<User>>> GetUsersAsync()
    {
        Result<IEnumerable<User>> users = await UserHelper.GetUsersFromDatabase(_applicationDbContext, _logger);
        return users.IsSuccess
            ? Result<IEnumerable<User>>.Success(users.Value)
            : Result<IEnumerable<User>>.Failure(users.Error);
    }
}