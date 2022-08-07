using FitnessTracker.Models.Fitness.Excercises;

namespace FitnessTracker.Models.Fitness.Datas;

public class CardioData : Data
{
    public double Distance { get; set; }
    public double Duration { get; set; }
    public ExerciseType Type => ExerciseType.Cardio;
}