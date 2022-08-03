using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Common;
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
        var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == loginParameters.Email);

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

        var response = new LoginResponse
        {
            User = user
        };
        return Result<LoginResponse>.Success(response);
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegistrationParameters registrationParameters)
    {
        var userInDatabase =
            await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == registrationParameters.Email);

        if (userInDatabase is not null)
        {
            var message = $"User with email {registrationParameters.Email} already exists.";
            logger.LogError(message);
            return Result<RegisterResponse>.Failure(message);
        }

        var user = registrationParameters.Adapt<User>();
        await applicationDbContext.Users.AddAsync(user);
        await applicationDbContext.SaveChangesAsync();

        var response = new RegisterResponse
        {
            User = user
        };
        return Result<RegisterResponse>.Success(response);
    }
}