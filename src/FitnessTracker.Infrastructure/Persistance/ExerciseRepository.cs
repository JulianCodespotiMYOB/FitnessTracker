using System.Reflection;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness;
using FitnessTracker.Models.Fitness.Excercises;
using Microsoft.Extensions.Caching.Memory;

namespace FitnessTracker.Infrastructure.Persistance;

public class ExerciseRepository : IExerciseRepository
{
    private const string ExercisesCacheKey = "Exercises";
    private readonly IMemoryCache _cache;

    public ExerciseRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Result<IEnumerable<Exercise>> GetExercises()
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

        string binDirectory = (Assembly.GetExecutingAssembly().Location ?? "").Replace("file:///", string.Empty);
        string csv = Path.Combine(Path.GetDirectoryName(binDirectory) ?? "", "Assets//exercises.csv");
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