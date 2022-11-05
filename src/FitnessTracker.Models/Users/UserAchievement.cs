using System.Text.Json.Serialization;
using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users.Enums;

namespace FitnessTracker.Models.Users;

[JsonDerivedType(typeof(WeightUserAchievement), typeDiscriminator: nameof(AchievementTypes.Weight))]
[JsonDerivedType(typeof(StreakUserAchievement), typeDiscriminator: nameof(AchievementTypes.Streak))]
[JsonDerivedType(typeof(LevelUserAchievement), typeDiscriminator: nameof(AchievementTypes.Level))]
[JsonDerivedType(typeof(DistanceUserAchievement), typeDiscriminator: nameof(AchievementTypes.Distance))]
[JsonDerivedType(typeof(RepsUserAchievement), typeDiscriminator: nameof(AchievementTypes.Reps))]
[JsonDerivedType(typeof(SetsUserAchievement), typeDiscriminator: nameof(AchievementTypes.Sets))]
public interface IUserAchievement : IAchievement
{
    public decimal Progress { get; set; }
    public bool IsCompleted { get; }
}

public class WeightUserAchievement : IUserAchievement
{
    public int Id { get; set; }
    public decimal TargetWeight { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public AchievementTypes AchievementType => AchievementTypes.Weight;
    public decimal Progress { get; set; }
    public bool IsCompleted => Progress >= TargetWeight;
    public WeightUserAchievement(WeightAchievement weightAchievement, List<Activity> activities)
    {
        Id = weightAchievement.Id;
        TargetWeight = weightAchievement.TargetWeight;
        TargetMuscleGroup = weightAchievement.TargetMuscleGroup;
        Title = weightAchievement.Title;
        Description = weightAchievement.Description;
        Rewards = weightAchievement.Rewards;

        Progress = activities
            .Where(a => a.Exercise.MainMuscleGroup == TargetMuscleGroup)
            .Sum(a => a.Data.Weight ?? 0);
    }
}

public class DistanceUserAchievement : IUserAchievement
{
    public int Id { get; set; }
    public decimal TargetDistance { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted => Progress >= TargetDistance;
    public AchievementTypes AchievementType => AchievementTypes.Distance;
    public DistanceUserAchievement(DistanceAchievement distanceAchievement, List<Activity> activities)
    {
        Id = distanceAchievement.Id;
        TargetDistance = distanceAchievement.TargetDistance;
        Title = distanceAchievement.Title;
        Description = distanceAchievement.Description;
        Rewards = distanceAchievement.Rewards;
        Progress = activities
            .Where(a => a.Exercise.MainMuscleGroup == MuscleGroup.Cardio)
            .Sum(a => a.Data.Distance ?? 0);
    }
}

public class SetsUserAchievement : IUserAchievement
{
    public int Id { get; set; }
    public int TargetSets { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted => Progress >= TargetSets;
    public AchievementTypes AchievementType => AchievementTypes.Sets;
    public SetsUserAchievement(SetsAchievement setsAchievement, List<Activity> activities)
    {
        Id = setsAchievement.Id;
        TargetSets = setsAchievement.TargetSets;
        TargetMuscleGroup = setsAchievement.TargetMuscleGroup;
        Title = setsAchievement.Title;
        Description = setsAchievement.Description;
        Rewards = setsAchievement.Rewards;
        Progress = activities
            .Where(a => a.Exercise.MainMuscleGroup == TargetMuscleGroup)
            .Sum(a => a.Data.Sets ?? 0);
    }
}

public class RepsUserAchievement : IUserAchievement
{
    public int Id { get; set; }
    public int TargetReps { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted => Progress >= TargetReps;
    public AchievementTypes AchievementType => AchievementTypes.Reps;
    public RepsUserAchievement(RepsAchievement repsAchievement, List<Activity> activities)
    {
        Id = repsAchievement.Id;
        TargetReps = repsAchievement.TargetReps;
        TargetMuscleGroup = repsAchievement.TargetMuscleGroup;
        Title = repsAchievement.Title;
        Description = repsAchievement.Description;
        Rewards = repsAchievement.Rewards;
        Progress = activities
            .Where(a => a.Exercise.MainMuscleGroup == TargetMuscleGroup)
            .Sum(a => a.Data.Reps ?? 0);
    }
}

public class LevelUserAchievement : IUserAchievement
{
    public int Id { get; set; }
    public decimal TargetLevel { get; set; }
    public StrengthLevelTypes TargetStrengthLevelType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted => Progress >= TargetLevel;
    public AchievementTypes AchievementType => AchievementTypes.Level;
    public LevelUserAchievement(LevelAchievement levelAchievement, Dictionary<StrengthLevelTypes, decimal> levelStats)
    {
        Id = levelAchievement.Id;
        TargetLevel = levelAchievement.TargetLevel;
        TargetStrengthLevelType = levelAchievement.TargetStrengthLevelType;
        Title = levelAchievement.Title;
        Description = levelAchievement.Description;
        Rewards = levelAchievement.Rewards;
        Progress = levelStats[TargetStrengthLevelType];
    }
}

public class StreakUserAchievement : IUserAchievement
{
    public int Id { get; set; }
    public int TargetStreak { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Reward> Rewards { get; set; }
    public decimal Progress { get; set; }
    public bool IsCompleted => Progress >= TargetStreak;
    public AchievementTypes AchievementType => AchievementTypes.Streak;
    public StreakUserAchievement(StreakAchievement achievement, int streak)
    {
        Id = achievement.Id;
        TargetStreak = achievement.TargetStreak;
        Title = achievement.Title;
        Description = achievement.Description;
        Rewards = achievement.Rewards;
        Progress = streak;
    }
}
