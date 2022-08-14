using FitnessTracker.Models.Fitness;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyBack : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Back;
    public int Level { get; set; }
}