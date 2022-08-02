using FitnessTracker.Models.Excercises.Workout;

namespace FitnessTracker.Interfaces;

public interface IWorkoutAggregateRoot
{
    void AddWorkout(Workout workout);
    void RemoveWorkout(Workout workout);
    void UpdateWorkout(Workout workout);
}