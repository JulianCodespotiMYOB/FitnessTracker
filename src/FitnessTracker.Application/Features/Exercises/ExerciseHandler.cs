using System.Diagnostics;
using FitnessTracker.Contracts.Responses.Exercises.GetExercises;
using FitnessTracker.Contracts.Responses.Exercises.PostExercises;
using FitnessTracker.Domain.Exercises;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services.Exercises;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Fitness.Exercises;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Exercises;

public class ExerciseHandler : IExerciseService
{
    private const string ExercisesCacheKey = "Exercises";
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMemoryCache _cache;
    private readonly ILogger _logger;

    public ExerciseHandler(ILogger<ExerciseHandler> logger, IApplicationDbContext applicationDbContext,
        IMemoryCache cache)
    {
        _cache = cache;
        _logger = logger;
        _applicationDbContext = applicationDbContext;
        _cache = cache;
    }

    public Result<GetExercisesResponse> GetExercises()
    {
        if (_cache.TryGetValue(ExercisesCacheKey, out List<Exercise>? cachedExercises))
        {
            return cachedExercises switch
            {
                null => Result<GetExercisesResponse>.Failure("Failed to load exercises."),
                _ => Result<GetExercisesResponse>.Success(new GetExercisesResponse(cachedExercises))
            };
        }

        List<Exercise> exercises = _applicationDbContext.Exercises.ToList();
        List<Exercise> filteredExercises = exercises.Where(x => x.MainMuscleGroup != MuscleGroup.Unknown).ToList();
        _cache.Set(ExercisesCacheKey, filteredExercises);

        GetExercisesResponse response = new(filteredExercises);
        return Result<GetExercisesResponse>.Success(response);
    }

    public async Task<Result<PostExercisesResponse>> PostExercisesAsync()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        Result<List<Exercise>> exercises = ExerciseScraper.ScrapeExercises();
        stopwatch.Stop();

        _logger.LogInformation($"Scraped exercises in {stopwatch.ElapsedMilliseconds}ms.");

        if (!exercises.IsSuccess)
        {
            return Result<PostExercisesResponse>.Failure(exercises.Error);
        }

        IEnumerable<Exercise> data = exercises.Value
            .Where(x => x.MainMuscleGroup != MuscleGroup.Unknown)
            .Where(x => !_applicationDbContext.Exercises.Any(y => y.Name == x.Name))
            .DistinctBy(x => x.Name)
            .ToList();

        await _applicationDbContext.Exercises.AddRangeAsync(data);
        await _applicationDbContext.SaveChangesAsync();

        PostExercisesResponse response = new(stopwatch.ElapsedMilliseconds);
        return Result<PostExercisesResponse>.Success(response);
    }
}
