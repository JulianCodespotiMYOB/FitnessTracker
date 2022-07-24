using FitnessTracker.Contracts.Requests.Dtos.Authorization;
using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Interfaces;
using FitnessTracker.Models;
using FitnessTracker.Models.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application;

public class AuthorizationHandler : IAuthorizationHandler
{
  private readonly IApplicationDbContext applicationDbContext;
  private readonly ILogger logger;

  public AuthorizationHandler(IApplicationDbContext applicationDbContext, ILogger<AuthorizationHandler> logger)
  {
    this.applicationDbContext = applicationDbContext;
    this.logger = logger;
  }

  public async Task<Result<User>> LoginAsync(LoginParameters loginParameters)
  {
    User? user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == loginParameters.Email);

    if (user is null)
    {
      string message = $"User with email {loginParameters.Email} does not exist.";
      logger.LogError(message);
      return Result<User>.Failure(message);
    }

    if (!user.Password.Equals(loginParameters.Password))
    {
      string message = $"Password for user with email {loginParameters.Email} is incorrect.";
      logger.LogError(message);
      return Result<User>.Failure(message);
    }

    return Result<User>.Success(user);
  }

  public async Task<Result<User>> RegisterAsync(RegistrationParameters registrationParameters)
  {
    User? user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == registrationParameters.Email);

    if (user is not null)
    {
      string message = $"User with email {registrationParameters.Email} already exists.";
      logger.LogError(message);
      return Result<User>.Failure(message);
    }

    User newUser = new(
      null,
      registrationParameters.Username,
      registrationParameters.Password,
      registrationParameters.Email,
      registrationParameters.FirstName,
      registrationParameters.LastName
    );

    await applicationDbContext.Users.AddAsync(newUser);
    await applicationDbContext.SaveChangesAsync();

    return Result<User>.Success(newUser.Adapt<User>());
  }
}