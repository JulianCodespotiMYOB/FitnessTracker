using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Models.Fitness.Datas;

public class Data
{
    public int Id { get; set; }
    public ExerciseType Type { get; set; }
    public decimal? Distance { get; set; }
    public decimal? Duration { get; set; }
    public int? Reps { get; set; }
    public int? Sets { get; set; }
    public decimal? Weight { get; set; }
    public decimal TargetDistance { get; set; }
    public decimal TargetDuration { get; set; }
    public int TargetReps { get; set; }
    public int TargetSets { get; set; }
    public decimal TargetWeight { get; set; }
    public string? Notes { get; set; }
    public Image? Image { get; set; }
}