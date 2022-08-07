using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Buddy.BuddyAnatomy.BuddyParts;

public class BuddyChest : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Chest;
    public int Level { get; set; }
}