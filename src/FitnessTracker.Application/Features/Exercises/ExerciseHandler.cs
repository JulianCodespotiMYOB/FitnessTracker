using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Muscles;
using Microsoft.Extensions.Logging;
using System;

namespace FitnessTracker.Application.Features.Workouts;

public class ExerciseHandler : IExerciseService
{
    private readonly IExerciseRepository exerciseRepository;
    private readonly ILogger logger;

    public ExerciseHandler(IExerciseRepository exerciseRepository, ILogger<ExerciseHandler> logger)
    {
        this.exerciseRepository = exerciseRepository;
        this.logger = logger;
    }

    public Result<GetExercisesResponse> GetExercises()
    {
        Result<IEnumerable<Exercise>> result = exerciseRepository.LoadExercises();

        if (result.IsSuccess is false)
        {
            logger.LogError($"Failed to load exercises: {result.Error}");
            return Result<GetExercisesResponse>.Failure(result.Error);
        }

        List<Exercise> cleanedExercises = result.Value
                .Where(x => x.PrimaryMuscleGroup != MuscleGroup.Unknown)
                .Select(x => new Exercise()
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

        GetExercisesResponse response = new()
        {
            Exercises = cleanedExercises
        };

        return Result<GetExercisesResponse>.Success(response);
    }
}