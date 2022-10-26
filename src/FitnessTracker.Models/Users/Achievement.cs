using System.Text.Json.Serialization;
using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Users;

public enum AchievementTypes
{
    Streak,
    Level,
    Weight,
    Distance,
    Reps,
    Sets,
}

[JsonDerivedType(typeof(WeightAchievement), typeDiscriminator: nameof(AchievementTypes.Weight))]
[JsonDerivedType(typeof(StreakAchievement), typeDiscriminator: nameof(AchievementTypes.Streak))]
[JsonDerivedType(typeof(LevelAchievement), typeDiscriminator: nameof(AchievementTypes.Level))]
[JsonDerivedType(typeof(DistanceAchievement), typeDiscriminator: nameof(AchievementTypes.Distance))]
[JsonDerivedType(typeof(RepsAchievement), typeDiscriminator: nameof(AchievementTypes.Reps))]
[JsonDerivedType(typeof(SetsAchievement), typeDiscriminator: nameof(AchievementTypes.Sets))]
public interface IAchievement
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<IReward> Rewards { get; set; }
    public AchievementTypes AchievementType { get; }
}

public class WeightAchievement : IAchievement
{
    public int Id { get; set; }
    public decimal TargetWeight { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<IReward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Weight;
}

public class DistanceAchievement : IAchievement
{
    public int Id { get; set; }
    public decimal TargetDistance { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<IReward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Distance;
}

public class SetsAchievement : IAchievement
{
    public int Id { get; set; }
    public int TargetSets { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<IReward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Sets;
}

public class RepsAchievement : IAchievement
{
    public int Id { get; set; }
    public int TargetReps { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<IReward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Reps;
}

public class LevelAchievement : IAchievement
{
    public int Id { get; set; }
    public decimal TargetLevel { get; set; }
    public StrengthLevelTypes TargetStrengthLevelType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<IReward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Level;
}

public class StreakAchievement : IAchievement
{
    public int Id { get; set; }
    public int TargetStreak { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<IReward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Streak;
}

public enum RewardTypes
{
    Experience,
    Title,
    Badge,
}

[JsonDerivedType(typeof(Experience), typeDiscriminator: nameof(RewardTypes.Experience))]
[JsonDerivedType(typeof(Title), typeDiscriminator: nameof(RewardTypes.Title))]
[JsonDerivedType(typeof(Badge), typeDiscriminator: nameof(RewardTypes.Badge))]
public interface IReward
{
    public int Id { get; set; }
    public RewardTypes RewardType { get; }
}

public class Title : IReward
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RewardTypes RewardType => RewardTypes.Title;
}

public class Badge : IReward
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }
    public RewardTypes RewardType => RewardTypes.Badge;
}

public class Experience : IReward
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public StrengthLevelTypes StrengthLevel { get; set; }
    public RewardTypes RewardType => RewardTypes.Experience;
}
