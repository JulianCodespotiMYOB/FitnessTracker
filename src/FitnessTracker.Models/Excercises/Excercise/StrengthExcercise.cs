using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Excercises.Excercise;

public class StrengthExcercise : Excercise
{
    public WorkoutType WorkoutType => WorkoutType.Strength;
    public List<MuscleGroup> MuscleGroups { get; set; }
}