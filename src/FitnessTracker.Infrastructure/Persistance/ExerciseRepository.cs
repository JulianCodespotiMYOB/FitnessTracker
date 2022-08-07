using FitnessTracker.Interfaces;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Excercises;
using FitnessTracker.Models.Muscles;
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
            return cachedExercises switch
            {
                null => Result<IEnumerable<Exercise>>.Failure("Failed to load exercises."),
                _ => Result<IEnumerable<Exercise>>.Success(cachedExercises)
            };

        List<Exercise> exercises = new();

        var csv = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName ?? "",
            "assets\\exercises.csv");
        var text = File.ReadAllText(csv);
        var lines = text.Split('\n');
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var exerciseName = values[0];
            var muscle = values[1];
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