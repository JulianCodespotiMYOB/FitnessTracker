using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.WorkoutBuddy;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Authorization;

public class UserHandler : IAuthorizationHandler
{
    private readonly IApplicationDbContext applicationDbContext;
    private readonly ILogger logger;

    public UserHandler(IApplicationDbContext applicationDbContext, ILogger<UserHandler> logger)
    {
        this.applicationDbContext = applicationDbContext;
        this.logger = logger;
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginParameters loginParameters)
    {
        User? user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == loginParameters.Email);

        if (user is null)
        {
            var message = $"User with email {loginParameters.Email} does not exist.";
            logger.LogError(message);
            return Result<LoginResponse>.Failure(message);
        }

        if (!user.Password.Equals(loginParameters.Password))
        {
            var message = $"Password for user with email {loginParameters.Email} is incorrect.";
            logger.LogError(message);
            return Result<LoginResponse>.Failure(message);
        }

        LoginResponse response = new()
        {
            User = user
        };
        return Result<LoginResponse>.Success(response);
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegistrationParameters registrationParameters)
    {
        User? userInDatabase =
            await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == registrationParameters.Email);

        if (userInDatabase is not null)
        {
            var message = $"User with email {registrationParameters.Email} already exists.";
            logger.LogError(message);
            return Result<RegisterResponse>.Failure(message);
        }

        WorkoutBuddy buddy = new()
        {
            Name = registrationParameters.BuddyName,
            Description = registrationParameters.BuddyDescription,
            IconId = registrationParameters.BuddyIconId
        };

        User user = registrationParameters.Adapt<User>();
        user.WorkoutBuddy = buddy;

        await applicationDbContext.Users.AddAsync(user);
        await applicationDbContext.SaveChangesAsync();

        RegisterResponse response = new()
        {
            User = user
        };
        return Result<RegisterResponse>.Success(response);
    }
}