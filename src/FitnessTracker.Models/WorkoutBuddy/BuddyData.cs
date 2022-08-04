using FitnessTracker.Models.WorkoutBuddy.BuddyAnatomy.BuddyParts;

namespace FitnessTracker.Models.WorkoutBuddy;

public class BuddyData
{
    public int Id { get; set; }
    public double Strength { get; set; } = 0;
    public double Stamina { get; set; } = 0;
    public double Flexibility { get; set; } = 0;
    public double Speed { get; set; } = 0;
    public double Power { get; set; } = 0;
    public double Muscle { get; set; } = 0;
    public int Streak { get; set; } = 0;
    public BuddyAbs AbsAnatomy { get; set; }
    public BuddyBiceps BicepsAnatomy { get; set; }
    public BuddyChest ChestAnatomy { get; set; }
    public BuddyForearms ForearmsAnatomy { get; set; }
    public BuddyGlutes GlutesAnatomy { get; set; }
    public BuddyTraps TrapsAnatomy { get; set; }
    public BuddyShoulders ShouldersAnatomy { get; set; }
    public BuddyLats LatsAnatomy { get; set; }
    public BuddyLegs LegsAnatomy { get; set; }
    public BuddyBack BackAnatomy { get; set; }
    public BuddyTriceps TricepsAnatomy { get; set; }
}