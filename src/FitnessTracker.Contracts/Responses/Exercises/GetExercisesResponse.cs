using FitnessTracker.Models.Fitness.Excercises;

namespace FitnessTracker.Contracts.Responses.Exercises;

public class GetExercisesResponse
{
    public IEnumerable<Exercise> Exercises { get; set; }
}