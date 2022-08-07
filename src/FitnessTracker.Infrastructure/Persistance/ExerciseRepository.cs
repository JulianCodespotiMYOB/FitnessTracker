using FitnessTracker.Interfaces;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Muscles;
using Microsoft.Extensions.Caching.Memory;

namespace FitnessTracker.Infrastructure.Persistance;

public class ExerciseRepository : IExerciseRepository
{
    private readonly IMemoryCache _cache;
    private const string ExercisesCacheKey = "Exercises";

    public ExerciseRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Result<IEnumerable<Exercise>> LoadExercises()
    {
        if (_cache.TryGetValue(ExercisesCacheKey, out List<Exercise>? cachedExercises))
        {
            return cachedExercises switch
            {
                null => Result<IEnumerable<Exercise>>.Failure("Failed to load exercises."),
                _ => Result<IEnumerable<Exercise>>.Success(cachedExercises)
            };
        }

        List<Exercise> exercises = new();

        string csv = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName ?? "","assets\\exercises.csv");
        string text = File.ReadAllText(csv);
        string[] lines = text.Split('\n');
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            string exerciseName = values[0];
            string muscle = values[1];
            MuscleGroup muscleGroup = MuscleGroupExtensions.FromName(muscle);

            Exercise exercise = new()
            {
                Name = exerciseName,
                Description = "",
                Type = ExerciseType.Strength,
                PrimaryMuscleGroup = muscleGroup
            };

            exercises.Add(exercise);
        }

        _cache.Set(ExercisesCacheKey, exercises);

        return Result<IEnumerable<Exercise>>.Success(exercises);
    }
}
