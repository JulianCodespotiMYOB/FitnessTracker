using System.Diagnostics;
using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Domain;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Exercises;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features;

public class ExerciseHandler : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger _logger;
    private const string ExercisesCacheKey = "Exercises";
    private readonly IMemoryCache _cache;

    public ExerciseHandler(IExerciseRepository exerciseRepository, ILogger<ExerciseHandler> logger, IApplicationDbContext applicationDbContext, IMemoryCache cache)
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
        _cache.Set(ExercisesCacheKey, exercises);
        GetExercisesResponse response = new(exercises);
        return Result<GetExercisesResponse>.Success(response);
    }

    public Result<PostExercisesResponse> PostExercises()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        List<Exercise> exercises = ExerciseScraper.ScrapeExercises();
        stopwatch.Stop();
        _applicationDbContext.Exercises.AddRange(exercises);
        _applicationDbContext.SaveChangesAsync();
        PostExercisesResponse response = new(stopwatch.ElapsedMilliseconds);
        return Result<PostExercisesResponse>.Success(response);
    }
}