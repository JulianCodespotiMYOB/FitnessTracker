using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Interfaces;
using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness;
using FitnessTracker.Models.Fitness.Excercises;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Features.Exercises;

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
        Result<IEnumerable<Exercise>> result = _exerciseRepository.GetExercises();
        if (result.IsSuccess is false)
        {
            _logger.LogError($"Failed to load exercises: {result.Error}");
            return Result<GetExercisesResponse>.Failure(result.Error);
        }

        List<Exercise> cleanedExercises = result.Value
            .Where(x => x.PrimaryMuscleGroup != MuscleGroup.Unknown)
            .Select(x => new Exercise
            {
                Id = x.Id,
                Type = x.Type,
                Name = x.Name
                    .Split(' ')
                    .Select(y => string.Concat(y[..1].ToUpper(), y.AsSpan(1)))
                    .Aggregate((a, b) => string.Concat(a, " ", b)),
                Description = x.Description,
                PrimaryMuscleGroup = x.PrimaryMuscleGroup
            })
            .ToList();

        GetExercisesResponse response = new(cleanedExercises);
        return Result<GetExercisesResponse>.Success(response);
    }
}