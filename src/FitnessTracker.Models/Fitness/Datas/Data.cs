using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Models.Fitness.Datas;

public class Data
{
    public int Id { get; set; }
    public ExerciseType Type { get; set; }
    public double? Distance { get; set; }
    public double? Duration { get; set; }
    public int? Reps { get; set; }
    public int? Sets { get; set; }
    public double? Weight { get; set; }
    public double TargetDistance { get; set; }
    public int TargetReps { get; set; }
    public int TargetSets { get; set; }
    public double TargetWeight { get; set; }
}