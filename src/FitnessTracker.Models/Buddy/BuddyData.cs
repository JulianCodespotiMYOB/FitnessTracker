using FitnessTracker.Models.Buddy.Anatomy;
using FitnessTracker.Models.Buddy.Anatomy.Parts;

namespace FitnessTracker.Models.Buddy;

public class BuddyData
{
    public int Id { get; set; }
    public double Strength { get; set; } = 0;
    public double Speed { get; set; } = 0;
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