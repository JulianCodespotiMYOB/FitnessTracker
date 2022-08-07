using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Models.Fitness.Datas;

public class StrengthData : Data
{
    public int Reps { get; set; }
    public int Sets { get; set; }
    public int Weight { get; set; }
    public ExerciseType Type => ExerciseType.Strength;
}