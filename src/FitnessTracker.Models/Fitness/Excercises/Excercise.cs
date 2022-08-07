using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Fitness.Excercises;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ExerciseType Type { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
}