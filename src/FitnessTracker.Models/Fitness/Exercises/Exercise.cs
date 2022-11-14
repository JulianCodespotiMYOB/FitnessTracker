using System.ComponentModel.DataAnnotations.Schema;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Fitness.Extensions;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Models.Fitness.Exercises;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ExerciseType Type { get; set; }
    public Image MuscleGroupImage { get; set; } = new();
    public MuscleGroup MainMuscleGroup { get; set; }
    public DetailedMuscleGroup? DetailedMuscleGroup { get; set; }
    public List<MuscleGroup>? OtherMuscleGroups { get; set; }
    public Mechanics Mechanics { get; set; }
    public Equipment Equipment { get; set; }
    public Dictionary<MuscleGroup, decimal> MuscleGroupStats => MuscleGroupExtensions.GetMuscleGroupScore(MainMuscleGroup, DetailedMuscleGroup, OtherMuscleGroups);
}