using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Contracts.Responses.Exercises;

public record GetExercisesResponse(List<Exercise> Exercises);