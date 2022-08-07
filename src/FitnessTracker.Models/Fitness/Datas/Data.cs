using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Models.Fitness.Datas;

public abstract class Data
{
    public int Id { get; set; }
    public ExerciseType Type { get; }
}