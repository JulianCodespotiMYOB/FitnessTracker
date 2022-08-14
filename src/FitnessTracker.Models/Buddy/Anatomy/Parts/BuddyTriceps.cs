using FitnessTracker.Models.Fitness;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyTriceps : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Triceps;
    public int Level { get; set; }
}