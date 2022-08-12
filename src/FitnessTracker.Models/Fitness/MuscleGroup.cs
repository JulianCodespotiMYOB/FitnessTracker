namespace FitnessTracker.Models.Fitness;

public enum MuscleGroup
{
    Unknown,
    Chest,
    Back,
    Shoulders,
    Legs,
    Abs,
    Biceps,
    Triceps,
    Forearms,
    Traps,
    Lats,
    Glutes
}

public static class MuscleGroupExtensions
{
    public static MuscleGroup FromName(string name)
    {
        string cleanedName = name.Trim().ToLower();

        foreach (MuscleGroup muscleGroup in Enum.GetValues(typeof(MuscleGroup)))
        {
            if (cleanedName.Contains(muscleGroup.ToString().ToLower()))
            {
                return muscleGroup;
            }
        }

        return MuscleGroup.Unknown;
    }
}