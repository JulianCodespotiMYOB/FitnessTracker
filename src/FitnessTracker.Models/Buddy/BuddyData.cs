using FitnessTracker.Models.Buddy.Anatomy;
using FitnessTracker.Models.Buddy.Anatomy.Parts;
using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Models.Buddy;

public class BuddyData
{
    public int Id { get; set; }
    public Dictionary<MuscleGroup, double> MuscleGroupStats { get; set; }
    public Dictionary<StrengthLevelTypes, double> LevelStats { get; set; }
    public int Streak { get; set; } = 0;
    public List<Achievement> Achievements { get; set; }

    public List<IBuddyAnatomy> Anatomy { get; set; } = new()
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
}