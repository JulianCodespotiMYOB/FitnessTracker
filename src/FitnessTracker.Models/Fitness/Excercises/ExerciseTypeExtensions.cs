namespace FitnessTracker.Models.Fitness.Exercises;

public static class ExerciseTypeExtensions
{
    public static ExerciseType FromName(string name)
    {
        string cleanedName = name.Trim().ToLower().Replace(" ", "");
        foreach (ExerciseType exerciseType in Enum.GetValues(typeof(ExerciseType)))
        {
            if (cleanedName.Contains(exerciseType.ToString().ToLower()))
            {
                return exerciseType;
            }
        }

        return ExerciseType.Unknown;
    }
}