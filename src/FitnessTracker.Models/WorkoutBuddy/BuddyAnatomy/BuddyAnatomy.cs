using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.WorkoutBuddy.BuddyAnatomy;

public class BuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup { get; set; }
    public int Level { get; set; } = 1;
}