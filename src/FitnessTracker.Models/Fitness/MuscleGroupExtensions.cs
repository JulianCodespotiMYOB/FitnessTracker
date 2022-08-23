namespace FitnessTracker.Models.Fitness;

public static class MuscleGroupExtensions
{
    public static MuscleGroup FromName(string? name)
    {
        if (name is null) return MuscleGroup.Unknown;
        string cleanedName = name.Trim().ToLower().Replace(" ", "");

        foreach (MuscleGroup muscleGroup in Enum.GetValues(typeof(MuscleGroup)))
            if (cleanedName.Contains(muscleGroup.ToString().ToLower()))
                return muscleGroup;

        return MuscleGroup.Unknown;
    }
}