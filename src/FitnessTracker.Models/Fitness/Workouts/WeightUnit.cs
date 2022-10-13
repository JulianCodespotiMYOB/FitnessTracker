namespace FitnessTracker.Models.Fitness.Workouts;

public enum WeightUnit
{
    Pounds,
    Kilograms
}

public static class WeightUnitExtensions
{
    public static string ToFriendlyString(this WeightUnit weightUnit)
    {
        return weightUnit switch
        {
            WeightUnit.Pounds => "Pounds",
            WeightUnit.Kilograms => "Kilograms",
            _ => throw new ArgumentOutOfRangeException(nameof(weightUnit), weightUnit, null)
        };
    }

    public static decimal Convert(this WeightUnit weightUnit, decimal value)
    {
        return weightUnit switch
        {
            WeightUnit.Pounds => ConvertToPounds(value),
            WeightUnit.Kilograms => ConvertToKilograms(value),
            _ => throw new ArgumentOutOfRangeException(nameof(weightUnit), weightUnit, null)
        };
    }

    private static decimal ConvertToPounds(decimal weight) => Math.Round(weight * 2.20462m, 2);
    private static decimal ConvertToKilograms(decimal weight) => Math.Round(weight / 2.20462m, 2);
}