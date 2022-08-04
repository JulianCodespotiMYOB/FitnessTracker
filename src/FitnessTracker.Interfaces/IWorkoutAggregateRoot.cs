using FitnessTracker.Models.Fitness.Workout;

namespace FitnessTracker.Interfaces;

public interface IWorkoutAggregateRoot
{
    void AddWorkout(Workout workout);
    void RemoveWorkout(Workout workout);
    void UpdateWorkout(Workout workout);
}