using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.User;

public interface IAchievementService
{
    public Task<Result<RecordAchievementResponse>> RecordAchievementAsync(int id, int achievementId);
    public Task ReverseAchievementAsync(int id, int achievementId);
}