namespace FitnessTracker.Models.Fitness.Datas;

public class StrengthData : Data
{
    public int Reps { get; set; }
    public int Sets { get; set; }
    public int Weight { get; set; }
    public WorkoutType WorkoutType => WorkoutType.Strength;
}