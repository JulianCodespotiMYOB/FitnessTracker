using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces;
public interface IExerciseService
{
    Result<GetExercisesResponse> GetExercises();
}