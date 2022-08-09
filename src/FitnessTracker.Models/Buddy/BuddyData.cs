using FitnessTracker.Models.Buddy.BuddyAnatomy;
using FitnessTracker.Models.Buddy.BuddyAnatomy.BuddyParts;

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
        new BuddyLegs(),
        new BuddyShoulders(),
        new BuddyForearms(),
        new BuddyGlutes(),
        new BuddyAbs(),
        new BuddyLats(),
        new BuddyTriceps(),
        new BuddyBiceps()
    };
}