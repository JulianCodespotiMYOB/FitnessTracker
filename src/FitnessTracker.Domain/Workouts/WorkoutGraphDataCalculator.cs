using FitnessTracker.Models.Fitness.GraphData;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Domain.Workouts;

public class WorkoutGraphDataCalculator
{
    public static List<WorkoutGraphData> GetWeightGraphData(User user, string workoutName, int reps)
    {
        List<WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts.Where(w => w.Completed))
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName && (activity.Data?.Reps ?? 0) == reps && activity.Data?.Weight != null)
                {
                    graphData.Add(new WorkoutGraphData
                    {
                        ExerciseMetaData = activity.Data.Weight.Value,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return graphData;
    }

    public static List<WorkoutGraphData> GetDistanceGraphData(User user, string workoutName)
    {
        List<WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts.Where(w => w.Completed)) 
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    decimal? distance = activity.Data.Distance;
                    if (distance == null) continue;
                    graphData.Add(new WorkoutGraphData
                    {
                        ExerciseMetaData = distance.Value,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return graphData;
    }

    public static List<WorkoutGraphData> GetRepsGraphData(User user, string workoutName)
    {
        List<WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts.Where(w => w.Completed)) 
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    int? reps = activity.Data.Reps;
                    if (reps == null) continue;
                    graphData.Add(new WorkoutGraphData
                    {
                        ExerciseMetaData = reps.Value,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return graphData;
    }

    public static List<WorkoutGraphData> GetSetsGraphData(User user, string workoutName)
    {
        List<WorkoutGraphData> graphData = new();
        int increment = 0;

        foreach (Workout workout in user.Workouts.Where(w => w.Completed))
        {
            foreach (Activity activity in workout.Activities)
            {
                if (activity.Exercise.Name == workoutName)
                {
                    int? sets = activity.Data.Sets;
                    if (sets == null) continue;
                    graphData.Add(new WorkoutGraphData
                    {
                        ExerciseMetaData = sets.Value,
                        TimeOfExercise = workout.Time,
                        XAxis = increment++
                    });
                }
            }
        }

        return graphData;
    }
}