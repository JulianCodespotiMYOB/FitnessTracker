using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Fitness.Excercises;

public class StrengthExcercise : Excercise
{
    public List<MuscleGroup> MuscleGroups { get; set; }
}