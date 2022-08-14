using FitnessTracker.Models.Fitness;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyLats : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Lats;
    public int Level { get; set; }
}