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
    private readonly IMemoryCache _cache;
    private readonly ILogger _logger;

    public ExerciseHandler(IMemoryCache cache, ILogger<ExerciseHandler> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public Result<GetExercisesResponse> GetExercises()
    {
        if (_cache.TryGetValue("Exercises", out List<Exercise>? cachedExercises))
        {
            if (cachedExercises is not null)
            {
                return Result<GetExercisesResponse>.Success(new GetExercisesResponse(cachedExercises));
            }
        }

        Result<List<Exercise>> exercises = ExerciseScraper.ScrapeExercises();
        if (!exercises.IsSuccess)
        {
            _logger.LogError(exercises.Error);
            return Result<GetExercisesResponse>.Failure("Failed to load exercises");
        }

        GetExercisesResponse response = new(exercises.Value);
        if (exercises.Value.Count == 0)
        {
            return Result<GetExercisesResponse>.Success(response);
        }

        _cache.Set("exercises", exercises, TimeSpan.FromDays(1));
        return Result<GetExercisesResponse>.Success(response);
    }
}