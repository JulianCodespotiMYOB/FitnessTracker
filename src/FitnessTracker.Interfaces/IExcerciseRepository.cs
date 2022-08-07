using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Interfaces;

public interface IExerciseRepository
{
    public Result<IEnumerable<Exercise>> LoadExercises();
}