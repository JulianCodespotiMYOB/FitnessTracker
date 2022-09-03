using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Contracts.Responses.Exercises.GetExercises;

public record GetExercisesResponse(List<Exercise> Exercises);