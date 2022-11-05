using FitnessTracker.Models.Users;

namespace FitnessTracker.Contracts.Requests.Users;

public record RecordAchievementResponse(IEnumerable<Reward> Rewards);