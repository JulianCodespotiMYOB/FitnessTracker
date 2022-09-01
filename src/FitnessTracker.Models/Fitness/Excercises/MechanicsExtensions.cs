namespace FitnessTracker.Models.Fitness.Excercises;

public static class MechanicsExtensions
{
    public static Mechanics FromName(string name)
    {
        string cleanedName = name.Trim().ToLower().Replace(" ", "");
        foreach (Mechanics mechanicType in Enum.GetValues(typeof(Mechanics)))
            if (cleanedName.Contains(mechanicType.ToString().ToLower()))
                return mechanicType;

        return Mechanics.Unknown;
    }
}