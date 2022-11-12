using FitnessTracker.Models.Buddy.Anatomy;
using FitnessTracker.Models.Buddy.Anatomy.Parts;
using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Models.Buddy;

public class BuddyData
{
    public int Id { get; init; }
    public Dictionary<MuscleGroup, decimal> MuscleGroupStats { get; init; }
    public Dictionary<StrengthLevelTypes, decimal> LevelStats { get; init; }
    public int Streak { get; init; } = 0;
    public List<IUserAchievement> UserAchievements { get; init; }
    public List<IBuddyAnatomy> Anatomy { get; init; }

    public BuddyData(
        int streak, 
        Dictionary<MuscleGroup, decimal> stats, 
        Dictionary<StrengthLevelTypes, decimal> levels, 
        Dictionary<MuscleGroup, int> anatomyLevels, 
        IEnumerable<IUserAchievement> achievements)
    {
        Streak = streak;
        UserAchievements = achievements.ToList();
        MuscleGroupStats = stats;
        LevelStats = levels;
        Anatomy = new()
        {
            new BuddyAbs(),
            new BuddyBack(),
            new BuddyChest(),
            new BuddyUpperLegs(),
            new BuddyLowerLegs(),
            new BuddyShoulders(),
            new BuddyForearm(),
            new BuddyGlutes(),
            new BuddyAbs(),
            new BuddyTriceps(),
            new BuddyBiceps()
        };

        foreach (IBuddyAnatomy muscle in Anatomy)
        {
            if (anatomyLevels.ContainsKey(muscle.MuscleGroup))
            {
                muscle.Level = anatomyLevels[muscle.MuscleGroup];
            }
        }
    }
}