using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Fitness.Workouts;

namespace FitnessTracker.Domain.Workouts;

public class ExerciseVolumeCalculator
{
    public static decimal CalculateVolumeForWorkout(Workout workout)
    {
        return workout.Activities.Sum(CalculateVolumeForActivity);
    }
    
    public static decimal CalculateVolumeForActivity(Activity activity)
    {
        if (IsNotWeighted(activity.Exercise.Type))
        {
            return (decimal) (activity.Data.Reps * activity.Data.Sets * activity.Data.Weight);
        }

        return 0;
        
        bool IsNotWeighted(ExerciseType exerciseType) => (exerciseType is ExerciseType.Stretching or ExerciseType.Cardio or ExerciseType.Unknown);
    }
}