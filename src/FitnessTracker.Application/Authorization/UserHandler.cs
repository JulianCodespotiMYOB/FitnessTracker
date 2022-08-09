using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Interfaces;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Authorization;

public class UserService : IAuthorizationService
{
    private readonly IApplicationDbContext applicationDbContext;
    private readonly ILogger logger;

    public UserService(IApplicationDbContext applicationDbContext, ILogger<UserService> logger)
    {
        this.applicationDbContext = applicationDbContext;
        this.logger = logger;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginParameters loginParameters)
    {
        Result<User> user = await UserHelper.GetUserFromDatabaseByEmail(loginParameters.Email, applicationDbContext, logger);

        if (user.IsSuccess is false)
        {
            return Result<LoginResponse>.Failure(user.Error);
        }

        if (!user.Value.Password.Equals(loginParameters.Password))
        {
            string message = $"Password for user with email {loginParameters.Email} is incorrect.";
            logger.LogError(message);
            return Result<LoginResponse>.Failure(message);
        }

        LoginResponse response = new()
        {
            User = user.Value
        };
        return Result<LoginResponse>.Success(response);
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegistrationParameters registrationParameters)
    {
        Result<User> user = await UserHelper.GetUserFromDatabaseByEmail(registrationParameters.Email, applicationDbContext, logger);

        if (user.IsSuccess is false)
        {
            return Result<RegisterResponse>.Failure(user.Error);
        }

        WorkoutBuddy buddy = new()
        {
            Name = registrationParameters.BuddyName,
            Description = registrationParameters.BuddyDescription,
            IconId = registrationParameters.BuddyIconId
        };

        user.Value.WorkoutBuddy = buddy;

        await applicationDbContext.Users.AddAsync(user.Value);
        await applicationDbContext.SaveChangesAsync();

        RegisterResponse response = new()
        {
            User = user.Value
        };
        return Result<RegisterResponse>.Success(response);
    }
}