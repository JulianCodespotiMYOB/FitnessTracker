using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using Mapster;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Users;

public class UserHandler : IUserService
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
        User? user = await UserHelper.GetUserFromDatabaseByEmail(request.Email, _applicationDbContext);
        if (user is null)
        {
            return Result<LoginResponse>.Failure("User not found");
        }

        if (!user.Password.Equals(request.Password))
        {
            string message = $"Password for user with email {request.Email} is incorrect.";
            _logger.LogError(message);
            return Result<LoginResponse>.Failure(message);
        }

        LoginResponse response = new(user);
        return Result<LoginResponse>.Success(response);
    }

    public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        User? user = await UserHelper.GetUserFromDatabaseByEmail(request.Email, _applicationDbContext);
        if (user is not null)
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
        User? user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext);
        return user is not null
            ? Result<GetUserResponse>.Success(new GetUserResponse(user))
            : Result<GetUserResponse>.Failure("User not found");
    }

    public async Task<Result<GetUsersResponse>> GetUsersAsync()
    {
        Result<IEnumerable<User>> users = await UserHelper.GetUsersFromDatabase(_applicationDbContext, _logger);
        return users.IsSuccess
            ? Result<GetUsersResponse>.Success(new GetUsersResponse(users.Value))
            : Result<GetUsersResponse>.Failure(users.Error);
    }

    public async Task<Result<UpdateSettingsResponse>> SetSettingsAsync(int id, UpdateSettingsRequest request)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext);
        if (user is null)
        {
            return Result<UpdateSettingsResponse>.Failure("User not found");
        }
        
        if (request.WeightUnit is not null)
        {
            user.UserSettings.WeightUnit = request.WeightUnit.Value;
        }

        if (request.MeasurementUnit is not null)
        {
            user.UserSettings.MeasurementUnit = request.MeasurementUnit.Value;
        }

        if (request.DarkMode is not null)
        {
            user.UserSettings.DarkMode = request.DarkMode.Value;
        }

        await _applicationDbContext.SaveChangesAsync();
        return Result<UpdateSettingsResponse>.Success(new UpdateSettingsResponse(user.UserSettings));
    }
}