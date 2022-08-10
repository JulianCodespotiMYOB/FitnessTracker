using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Buddy.BuddyAnatomy.BuddyParts;

public class BuddyForearms : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Forearms;
    public int Level { get; set; }
}