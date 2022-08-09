using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services;

public interface IExerciseService
{
    Result<GetExercisesResponse> GetExercises();
}