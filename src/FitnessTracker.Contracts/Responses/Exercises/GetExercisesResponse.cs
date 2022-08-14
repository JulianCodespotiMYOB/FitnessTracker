using FitnessTracker.Models.Fitness.Excercises;

namespace FitnessTracker.Contracts.Responses.Exercises;

public record GetExercisesResponse(IEnumerable<Exercise> Exercises);