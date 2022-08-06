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
    public BuddyAbs AbsAnatomy { get; set; } = new();
    public BuddyBiceps BicepsAnatomy { get; set; } = new();
    public BuddyChest ChestAnatomy { get; set; } = new();
    public BuddyForearms ForearmsAnatomy { get; set; } = new();
    public BuddyGlutes GlutesAnatomy { get; set; } = new();
    public BuddyTraps TrapsAnatomy { get; set; } = new();
    public BuddyShoulders ShouldersAnatomy { get; set; } = new();
    public BuddyLats LatsAnatomy { get; set; } = new();
    public BuddyLegs LegsAnatomy { get; set; } = new();
    public BuddyBack BackAnatomy { get; set; } = new();
    public BuddyTriceps TricepsAnatomy { get; set; } = new();
}