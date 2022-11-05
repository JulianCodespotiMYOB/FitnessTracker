using System.Text.Json.Serialization;
using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Users.Enums;

namespace FitnessTracker.Models.Users;

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
    public IEnumerable<Reward> Rewards { get; set; }
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
    public IEnumerable<Reward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Weight;
}

public class DistanceAchievement : IAchievement
{
    public int Id { get; set; }
    public decimal TargetDistance { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
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
    public IEnumerable<Reward> Rewards { get; set; }
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
    public IEnumerable<Reward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Reps;
}

public class LevelAchievement : IAchievement
{
    public int Id { get; set; }
    public decimal TargetLevel { get; set; }
    public StrengthLevelTypes TargetStrengthLevelType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Level;
}

public class StreakAchievement : IAchievement
{
    public int Id { get; set; }
    public int TargetStreak { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Streak;
}
