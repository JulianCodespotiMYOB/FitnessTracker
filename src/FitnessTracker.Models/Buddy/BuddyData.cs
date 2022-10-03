using FitnessTracker.Models.Buddy.Anatomy;
using FitnessTracker.Models.Buddy.Anatomy.Parts;
using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Buddy;

public class BuddyData
{
    public int Id { get; set; }
    public Dictionary<MuscleGroup, double> MuscleGroupStats { get; set; }
    public double Level { get; set; }
    public double PowerliftingLevel { get; set; }
    public double BodybuildingLevel { get; set; }
    public double WeightliftingLevel { get; set; }
    public int Streak { get; set; } = 0;

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