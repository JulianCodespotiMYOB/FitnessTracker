namespace FitnessTracker.Models.Fitness.Datas;

public class CardioData : Data
{
    public double Distance { get; set; }
    public double Duration { get; set; }
    public WorkoutType WorkoutType => WorkoutType.Cardio;
}