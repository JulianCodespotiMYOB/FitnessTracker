using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Buddy.BuddyAnatomy.BuddyParts;

public class BuddyTriceps : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Triceps;
    public int Level { get; set; }
}