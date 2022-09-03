using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyShoulders : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Shoulders;
    public int Level { get; set; }
}