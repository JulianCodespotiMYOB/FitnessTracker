namespace FitnessTracker.Models.Fitness.Exercises;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ExerciseType Type { get; set; }
    public MuscleGroup MainMuscleGroup { get; set; }
    public MuscleGroup? DetailedMuscleGroup { get; set; }
    public List<MuscleGroup>? OtherMuscleGroups { get; set; }
    public Mechanics Mechanics { get; set; }
    public Equipment Equipment { get; set; }
}