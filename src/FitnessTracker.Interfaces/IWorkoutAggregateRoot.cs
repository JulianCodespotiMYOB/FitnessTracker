using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Interfaces;

public interface IWorkoutAggregateRoot
{
    void AddWorkout(Workout workout);
    void RemoveWorkout(Workout workout);
    void UpdateWorkout(Workout workout);
}