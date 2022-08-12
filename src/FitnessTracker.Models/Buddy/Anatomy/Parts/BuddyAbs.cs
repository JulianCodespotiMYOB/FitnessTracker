using FitnessTracker.Models.Fitness;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyAbs : IBuddyAnatomy
{
    public int Id { get; set; }
    public int Level { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Abs;
}