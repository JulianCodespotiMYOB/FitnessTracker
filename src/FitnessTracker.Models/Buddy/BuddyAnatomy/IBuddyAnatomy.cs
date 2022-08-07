using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Buddy.BuddyAnatomy;

public interface IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup { get; }
    public int Level { get; set; }
}