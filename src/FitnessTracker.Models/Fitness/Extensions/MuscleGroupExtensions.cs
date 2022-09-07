using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Fitness.Extensions;

public static class MuscleGroupExtensions
{
    public static MuscleGroup FromName(string? name)
    {
        if (name is null)
        {
            return MuscleGroup.Unknown;
        }

        string cleanedName = name.Trim().ToLower().Replace(" ", "");

        foreach (MuscleGroup muscleGroup in Enum.GetValues(typeof(MuscleGroup)))
        {
            if (cleanedName.Contains(muscleGroup.ToString().ToLower()))
            {
                return muscleGroup;
            }
        }

        return MuscleGroup.Unknown;
    }

    public static DetailedMuscleGroup? FromNameDetailed(string? name)
    {
        if (name is null)
        {
            return null;
        }

        string cleanedName = name.Trim().ToLower().Replace(" ", "");

        foreach (DetailedMuscleGroup muscleGroup in Enum.GetValues(typeof(DetailedMuscleGroup)))
        {
            if (cleanedName.Contains(muscleGroup.ToString().ToLower()))
            {
                return muscleGroup;
            }
        }

        return null;
    }

    public static Dictionary<MuscleGroup, double> GetMuscleGroupScore(MuscleGroup mainMuscleGroup,
        DetailedMuscleGroup? detailedMuscleGroup,
        List<MuscleGroup>? otherMuscleGroups)
    {
        Dictionary<MuscleGroup, double> muscleGroupScore = new();

        foreach (MuscleGroup muscleGroup in Enum.GetValues(typeof(MuscleGroup)))
        {
            if (muscleGroup == MuscleGroup.Unknown)
            {
                continue;
            }

            double score = 0;

            if (muscleGroup == mainMuscleGroup)
            {
                score += 100;
            }

            if (detailedMuscleGroup is not null)
            {
                if (MuscleGroupContainsDetailedMuscleGroup(muscleGroup, detailedMuscleGroup.Value))
                {
                    score += 50;
                }
            }

            if (otherMuscleGroups != null && otherMuscleGroups.Contains(muscleGroup))
            {
                score += 25;
            }

            muscleGroupScore.Add(muscleGroup, score);
        }

        return muscleGroupScore.Where(x => x.Value > 0).ToDictionary(x => x.Key, x => x.Value);

        bool MuscleGroupContainsDetailedMuscleGroup(MuscleGroup muscleGroup, DetailedMuscleGroup? detailedMuscleGroup)
        {
            return muscleGroup switch
            {
                MuscleGroup.Abs => detailedMuscleGroup is
                    DetailedMuscleGroup.Abs or
                    DetailedMuscleGroup.LowerAbs or
                    DetailedMuscleGroup.UpperAbs or
                    DetailedMuscleGroup.Obliques,

                MuscleGroup.Back => detailedMuscleGroup is
                    DetailedMuscleGroup.LowerBack or
                    DetailedMuscleGroup.UpperBack or
                    DetailedMuscleGroup.Traps or
                    DetailedMuscleGroup.Lats,

                MuscleGroup.Chest => detailedMuscleGroup is
                    DetailedMuscleGroup.InnerChest or
                    DetailedMuscleGroup.OuterChest or
                    DetailedMuscleGroup.UpperChest or
                    DetailedMuscleGroup.LowerChest,

                MuscleGroup.UpperLegs => detailedMuscleGroup == DetailedMuscleGroup.Hamstrings,
                MuscleGroup.LowerLegs => detailedMuscleGroup == DetailedMuscleGroup.Calves,
                _ => false
            };
        }
    }
}