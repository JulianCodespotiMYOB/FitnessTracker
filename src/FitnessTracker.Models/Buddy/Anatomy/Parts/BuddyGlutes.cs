using FitnessTracker.Models.Fitness;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyGlutes : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Glutes;
    public int Level { get; set; }
}