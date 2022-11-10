using FitnessTracker.Contracts.Responses.Exercises.GetExercises;
using FitnessTracker.Contracts.Responses.Exercises.PostExercises;
using FitnessTracker.Domain.Exercises;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
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
    private readonly IMuscleGroupImagesService _muscleGroupImagesService;
    public ExerciseHandler(ILogger<ExerciseHandler> logger, IApplicationDbContext applicationDbContext,
        IMemoryCache cache, IMuscleGroupImagesService muscleGroupImagesService)
    {
        _cache = cache;
        _logger = logger;
        _applicationDbContext = applicationDbContext;
        _cache = cache;
        _muscleGroupImagesService = muscleGroupImagesService;
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
                    List<Exercise> exercisesToAdd = data.Where(x => x.MainMuscleGroup != MuscleGroup.Unknown).ToList();
                    IEnumerable<Exercise> uniqueData = _applicationDbContext.Exercises
                        .Where(x => !exercisesToAdd.Select(y => y.Name).Contains(x.Name))
                        .ToList();
                    var exercisesToSave = exercisesToAdd.Concat(uniqueData).ToList();
                    foreach(Exercise exercise in exercisesToSave)
                    {
                        exercise.MuscleGroupImage = _muscleGroupImagesService.GetImageForMuscleGroups(exercise.MainMuscleGroup, exercise.OtherMuscleGroups);
                    }
                    return exercisesToSave;
                })
            .Match(
                async data =>
                {
                    _applicationDbContext.Exercises.RemoveRange();
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
