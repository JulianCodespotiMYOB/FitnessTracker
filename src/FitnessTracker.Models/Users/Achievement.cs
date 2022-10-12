using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Users;

public abstract class Achievement
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public List<Reward> Rewards { get; set; } = new();
}

public class WeightAchievement : Achievement
{
    public decimal TargetWeight { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
}

public class DistanceAchievement : Achievement
{
    public decimal TargetDistance { get; set; }
}

public class SetsAchievement : Achievement
{
    public int TargetSets { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
}

public class RepsAchievement : Achievement
{
    public int TargetReps { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
}

public class LevelAchievement : Achievement
{
    public decimal TargetLevel { get; set; }
    public StrengthLevelTypes TargetStrengthLevelType { get; set; }
}

public class StreakAchievement : Achievement
{
   public int TargetStreak { get; set; }
}

public abstract class Reward
{
    public int Id { get; set; }
}

public class Title : Reward
{
    public string Name { get; set; }
}

public class Badge : Reward
{
    public string Name { get; set; }
    public Image Image { get; set; }
}

public class Experience : Reward
{
    public int Amount { get; set; }
    public StrengthLevelTypes StrengthLevel { get; set; }
}
