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
    public double TargetWeight { get; set; }
    public MuscleGroup TargetMuscleGroup { get; set; }
    public bool HasTargetMuscleGroup => TargetMuscleGroup != MuscleGroup.Unknown;
}

public class DistanceAchievement : Achievement
{
    public double TargetDistance { get; set; }
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
    public double TargetLevel { get; set; }
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

public static class Achievements
{
    public static List<Achievement> GetAll()
    {
        List<Achievement> achievements = new();
        achievements.Add(new StreakAchievement()
        {
            Rewards = new List<Reward>()
            {
                new Experience()
                {
                    Amount = 100,
                    StrengthLevel = StrengthLevelTypes.Bodybuilding
                }
            },
            TargetStreak = 5,
            Title = "5 Day Streak"
        });
        achievements.Add(new StreakAchievement()
        {
            Rewards = new List<Reward>()
            {
                new Experience()
                {
                    Amount = 200,
                    StrengthLevel = StrengthLevelTypes.Bodybuilding
                }
            },
            TargetStreak = 10,
            Title = "10 Day Streak"
        });
        achievements.Add(new StreakAchievement()
        {
            Rewards = new List<Reward>()
            {
                new Experience()
                {
                    Amount = 300,
                    StrengthLevel = StrengthLevelTypes.Bodybuilding
                }
            },
            TargetStreak = 15,
            Title = "15 Day Streak"
        });
        return achievements;
    }
}