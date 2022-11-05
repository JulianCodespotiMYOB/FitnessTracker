using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Buddy;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Workouts;
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

    public async Task<GetUsersResponse> GetUsersAsync() =>
        new(await UserHelper.GetUsersFromDatabase(_applicationDbContext));

    public async Task<Result<UpdateUserResponse>> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext);
        if (user is null)
        {
            return Result<UpdateUserResponse>.Failure("User not found");
        }

        if (user.UserSettings.WeightUnit != request.WeightUnit)
        {
            foreach (Activity activity in user.Workouts.SelectMany(workout => workout.Activities))
            {
                if (activity.Data.Weight is not null)
                {
                    activity.Data.Weight = request.WeightUnit switch
                    {
                        WeightUnit.Kilograms => WeightUnit.Kilograms.Convert(activity.Data.Weight.Value),
                        WeightUnit.Pounds => WeightUnit.Pounds.Convert(activity.Data.Weight.Value),
                        _ => throw new ArgumentOutOfRangeException(nameof(request.WeightUnit), request.WeightUnit, null)
                    };
                }

                activity.Data.TargetWeight = request.WeightUnit switch
                {
                    WeightUnit.Kilograms => WeightUnit.Kilograms.Convert(activity.Data.TargetWeight),
                    WeightUnit.Pounds => WeightUnit.Pounds.Convert(activity.Data.TargetWeight),
                    _ => throw new ArgumentOutOfRangeException(nameof(request.WeightUnit), request.WeightUnit, null)
                };
            }
        }

        int imageId = request.Avatar?.Id ?? -1;
        Image? existingImage = _applicationDbContext.Images.FirstOrDefault(i => i.Id == imageId);

        user.UserSettings.WeightUnit = request.WeightUnit;
        user.UserSettings.MeasurementUnit = request.MeasurementUnit;
        user.UserSettings.DarkMode = request.DarkMode;
        user.Username = request.Username;
        user.Email = request.Email;
        user.WeeklyWorkoutAmountGoal = request.WeeklyWorkoutAmountGoal;
        user.Height = request.Height;
        user.Weight = request.Weight;
        user.Age = request.Age;
        user.Avatar = existingImage ?? request.Avatar;
        user.ClaimedAchievements = request.ClaimedAchievements;

        await _applicationDbContext.SaveChangesAsync();
        return Result<UpdateUserResponse>.Success(new UpdateUserResponse(user.UserSettings));
    }

    public async Task<Result<RecordAchievementResponse>> RecordAchievementAsync(int id, int achievementId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext);
        if (user is null)
        {
            return Result<RecordAchievementResponse>.Failure("User not found");
        }

        if (user.ClaimedAchievements.Any(id => id == achievementId))
        {
            return Result<RecordAchievementResponse>.Failure("Achievement already claimed");
        }

        List<IUserAchievement> userAchievements = user.WorkoutBuddy.Data.UserAchievements;
        IUserAchievement? achievement = userAchievements.FirstOrDefault(ua => ua.Id == achievementId);
        if (achievement is null)
        {
            return Result<RecordAchievementResponse>.Failure("Achievement not found");
        }

        if (!achievement.IsCompleted)
        {
            return Result<RecordAchievementResponse>.Failure("Achievement not completed");
        }

        user.ClaimedAchievements.Add(achievement.Id);

        string? errorClaimingRewards = ClaimRewards(achievement.Rewards);
        if (errorClaimingRewards is not null)
        {
            return Result<RecordAchievementResponse>.Failure(errorClaimingRewards);
        }

        await _applicationDbContext.SaveChangesAsync();

        return Result<RecordAchievementResponse>.Success(new RecordAchievementResponse(achievement.Rewards));
    }

    private string? ClaimRewards(IEnumerable<Reward> rewards)
    {
        foreach (Reward reward in rewards)
        {
            switch (reward)
            {
                case Title title:
                    _applicationDbContext.Rewards.Add(title);
                    break;
                case Badge badge:
                    _applicationDbContext.Rewards.Add(badge);
                    break;
                case Experience experience:
                    break;
                default:
                    return $"Reward type {reward.GetType()} not supported";
            }
        }

        return null;
    }
}