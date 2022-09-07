using FitnessTracker.Models.Buddy.Anatomy;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Models.Buddy;

public class WorkoutBuddy
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = "No description";
    public int IconId { get; set; }
    public BuddyData Data => GetWorkoutBuddyData();

    private BuddyData GetWorkoutBuddyData()
    {
        BuddyData buddyData = new();
        buddyData.Streak = GetWorkoutBuddyStreak();
        buddyData.MuscleGroupStats = GetWorkoutBuddyMuscleGroupStats();

        SetBuddyAnatomyLevel(buddyData);

        return buddyData;
    }

    private List<Exercise> GetExercises()
    {
        return (from workout in User.Workouts from activity in workout.Activities select activity.Exercise).ToList();
    }

    private List<Activity> GetActivities()
    {
        return User.Workouts.SelectMany(workout => workout.Activities).ToList();
    }

    private int GetWorkoutBuddyStreak()
    {
        if (User.Workouts.Count <= User.WeeklyWorkoutAmountGoal) return 0;

        int currentStreak = 1;
        int daysWorkedOutInCurrentWeek = 0;
        DateTimeOffset previousWorkoutInCurrentWeek = User.Workouts.FirstOrDefault()!.Time;

        foreach (Workout workout in User.Workouts)
        {
            if (IsWorkoutInCurrentWeek(workout)) daysWorkedOutInCurrentWeek++;

            if (IsWorkoutInNextWeek(workout))
            {
                if (UserHasReachedGoal()) currentStreak++;

                if (UserHasNotReachedGoal()) currentStreak = 0;

                daysWorkedOutInCurrentWeek = 0;
                previousWorkoutInCurrentWeek = previousWorkoutInCurrentWeek.AddDays(7);
            }
        }

        return currentStreak;

        bool IsWorkoutInNextWeek(Workout workout)
        {
            return workout.Time >= previousWorkoutInCurrentWeek.AddDays(7);
        }

        bool IsWorkoutInCurrentWeek(Workout workout)
        {
            return workout.Time < previousWorkoutInCurrentWeek.AddDays(7);
        }

        bool UserHasReachedGoal()
        {
            return daysWorkedOutInCurrentWeek >= User.WeeklyWorkoutAmountGoal;
        }

        bool UserHasNotReachedGoal()
        {
            return daysWorkedOutInCurrentWeek < User.WeeklyWorkoutAmountGoal;
        }
    }

    private Dictionary<MuscleGroup, double> GetWorkoutBuddyMuscleGroupStats()
    {
        List<Activity> activities = GetActivities();
        Dictionary<MuscleGroup, double> muscleGroupStats = new();
        foreach (Activity activity in activities)
        {
            Dictionary<MuscleGroup, double> exerciseMuscleGroupStats = activity.Exercise.MuscleGroupStats;
            foreach (KeyValuePair<MuscleGroup, double> muscleGroupStat in exerciseMuscleGroupStats)
                if (muscleGroupStats.ContainsKey(muscleGroupStat.Key))
                    muscleGroupStats[muscleGroupStat.Key] += muscleGroupStat.Value;
                else
                    muscleGroupStats.Add(muscleGroupStat.Key, muscleGroupStat.Value);
        }

        return muscleGroupStats;
    }

    private void SetBuddyAnatomyLevel(BuddyData buddyData)
    {
        List<Exercise> exercises = GetExercises();

        foreach (IBuddyAnatomy buddyAnatomy in buddyData.Anatomy)
        {
            buddyAnatomy.Level = GetAnatomyLevel(buddyAnatomy.MuscleGroup);
        }

        int GetAnatomyLevel(MuscleGroup anatomyType)
        {
            return exercises.Count(exercise => exercise.MainMuscleGroup == anatomyType);
        }
    }
}