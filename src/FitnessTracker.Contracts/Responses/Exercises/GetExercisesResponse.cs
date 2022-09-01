using FitnessTracker.Models.Fitness.Excercises;

namespace FitnessTracker.Contracts.Responses.Exercises;

public record GetExercisesResponse(List<Exercise> Exercises);