using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Buddy.BuddyAnatomy.BuddyParts;

public class BuddyBack : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Back;
    public int Level { get; set; }
}