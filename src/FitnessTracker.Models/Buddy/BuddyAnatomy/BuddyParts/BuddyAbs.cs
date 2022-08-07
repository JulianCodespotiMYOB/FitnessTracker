using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Buddy.BuddyAnatomy.BuddyParts;

public class BuddyAbs : IBuddyAnatomy
{
    public int Id { get; set; }
    public int Level { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Abs;
}