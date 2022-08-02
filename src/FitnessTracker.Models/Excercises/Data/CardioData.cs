namespace FitnessTracker.Models.Excercises.Data;

public class CardioData : Data
{
    public double Distance { get; set; }
    public double Duration { get; set; }
    public WorkoutType WorkoutType => WorkoutType.Cardio;
}