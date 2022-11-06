using FitnessTracker.Application.Common;
using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Users;

public class AchievementHandler : IAchievementService
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;

    public AchievementHandler(IApplicationDbContext applicationDbContext, ILogger<AchievementHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<Result<RecordAchievementResponse>> RecordAchievementAsync(int id, int achievementId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext);
        if (user is null)
        {
            _logger.LogError($"User with id {id} not found.");
            return Result<RecordAchievementResponse>.Failure("User not found");
        }

        if (user.ClaimedAchievements.Any(id => id == achievementId))
        {
            _logger.LogError($"User with id {id} already has achievement with id {achievementId}.");
            return Result<RecordAchievementResponse>.Failure("Achievement already claimed");
        }

        List<IUserAchievement> userAchievements = user.WorkoutBuddy.Data.UserAchievements;
        IUserAchievement? achievement = userAchievements.FirstOrDefault(ua => ua.Id == achievementId);
        if (achievement is null)
        {
            _logger.LogError($"User with id {id} does not have achievement with id {achievementId}.");
            return Result<RecordAchievementResponse>.Failure("Achievement not found");
        }

        if (!achievement.IsCompleted)
        {
            _logger.LogError($"User with id {id} has not completed achievement with id {achievementId}.");
            return Result<RecordAchievementResponse>.Failure("Achievement not completed");
        }

        user.ClaimedAchievements.Add(achievement.Id);

        string? errorClaimingRewards = ClaimRewards(achievement.Rewards, user);
        if (errorClaimingRewards is not null)
        {
            _logger.LogError(errorClaimingRewards);
            return Result<RecordAchievementResponse>.Failure(errorClaimingRewards);
        }

        await _applicationDbContext.SaveChangesAsync();

        return Result<RecordAchievementResponse>.Success(new RecordAchievementResponse(achievement.Rewards));
    }

    public async Task ReverseAchievementAsync(int id, int achievementId)
    {
        User? user = await UserHelper.GetUserFromDatabaseById(id, _applicationDbContext);
        if (user is null)
        {
            _logger.LogError($"User with id {id} not found.");
            return;
        }

        if (!user.ClaimedAchievements.Any(id => id == achievementId))
        {
            _logger.LogError($"User with id {id} does not have achievement with id {achievementId}.");
            return;
        }

        user.ClaimedAchievements.Remove(achievementId);

        await _applicationDbContext.SaveChangesAsync();
    }

    private string? ClaimRewards(IEnumerable<Reward> rewards, User user)
    {
        foreach (Reward reward in rewards)
        {
            switch (reward)
            {
                case Title title:
                    user.Inventory.Add(title);
                    break;
                case Badge badge:
                    user.Inventory.Add(badge);
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