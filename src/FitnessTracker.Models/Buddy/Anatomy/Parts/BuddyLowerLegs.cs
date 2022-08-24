using FitnessTracker.Models.Fitness;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyLowerLegs : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.LowerLegs;
    public int Level { get; set; }
}