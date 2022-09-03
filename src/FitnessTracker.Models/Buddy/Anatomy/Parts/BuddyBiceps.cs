using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyBiceps : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Biceps;
    public int Level { get; set; }
}