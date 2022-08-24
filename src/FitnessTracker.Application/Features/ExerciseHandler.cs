using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Domain;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Exercises;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features;

public class ExerciseHandler : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ILogger _logger;

    public ExerciseHandler(IExerciseRepository exerciseRepository, ILogger<ExerciseHandler> logger)
    {
        _exerciseRepository = exerciseRepository;
        _logger = logger;
    }

    public Result<GetExercisesResponse> GetExercises()
    {
        ExerciseScraper exerciseScraper = new();
        List<Exercise> exercises = ExerciseScraper.ScrapeExercises();

        GetExercisesResponse response = new(exercises);
        return Result<GetExercisesResponse>.Success(response);
    }
}