using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Contracts.Responses.Exercises;

public class GetExercisesResponse
{
    public IEnumerable<Exercise> Exercises { get; set; }
}
