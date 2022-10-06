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
        Timed<Result<List<Exercise>>> exercises = Timed<Result<List<Exercise>>>.Record(ExerciseScraper.ScrapeExercises);

        return await exercises.Result
            .Map(
                data =>
                {
                    IEnumerable<Exercise> filteredData = data.Where(x => x.MainMuscleGroup != MuscleGroup.Unknown)
                        .Where(x => !_applicationDbContext.Exercises.Any(y => y.Name == x.Name))
                        .DistinctBy(x => x.Name);
                    return filteredData;
                })
            .Match(
                async data =>
                {
                    _applicationDbContext.Exercises.AddRange(data);
                    await _applicationDbContext.SaveChangesAsync();
                    return Result<PostExercisesResponse>.Success(new PostExercisesResponse(exercises.Time));
                },
                error =>
                {
                    _logger.LogError($"Failed to scrape exercises. Error: {error}");
                    return Task.FromResult(Result<PostExercisesResponse>.Failure(error));
                }
            );
    }
}
