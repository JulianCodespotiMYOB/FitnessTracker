using FitnessTracker.Contracts.Responses.Exercises.GetExercises;
using FitnessTracker.Contracts.Responses.Exercises.PostExercises;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.Exercises;

public interface IExerciseService
{
    Result<GetExercisesResponse> GetExercises();
    Task<Result<PostExercisesResponse>> PostExercisesAsync();
}