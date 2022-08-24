namespace FitnessTracker.Models.Fitness.Exercises;

public static class EquipmentExtensions
{
    public static Equipment FromName(string name)
    {
        string cleanedName = name.Trim().ToLower().Replace(" ", "").Replace("-", "");
        foreach (Equipment equipmentType in Enum.GetValues(typeof(Equipment)))
        {
            if (cleanedName.Contains(equipmentType.ToString().ToLower()))
            {
                return equipmentType;
            }
        }

        return Equipment.Unknown;
    }
}