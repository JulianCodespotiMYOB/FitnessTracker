using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Interfaces.Infrastructure;

public interface IExerciseRepository
{
    public Result<IEnumerable<Exercise>> GetExercises();
}