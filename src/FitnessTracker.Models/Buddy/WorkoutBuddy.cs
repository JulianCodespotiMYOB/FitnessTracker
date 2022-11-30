using FitnessTracker.Models.Buddy.Anatomy;
using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Datas;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Models.Buddy;

public class WorkoutBuddy
{
    public int Id { get; set; }
    public User User { get; set; } = null!;
    public string Name { get; set; } = null!;
    public BuddyData Data => GetWorkoutBuddyData();

    private BuddyData GetWorkoutBuddyData()
    {
        var streak = GetWorkoutBuddyStreak();
        var stats = GetWorkoutBuddyMuscleGroupStats();
        var levels = GetBuddyOverallLevels();
        var anatomyLevels = GetBuddyAnatomyLevel();
        var achievements = GetBuddyAchievements(streak, levels);

        return new BuddyData(streak, stats, levels, anatomyLevels, achievements);
    }

    private int GetWorkoutBuddyStreak()
    {
        if (User.Workouts.Count <= User.WeeklyWorkoutAmountGoal)
        {
            return 0;
        }

        int currentStreak = 1;
        int daysWorkedOutInCurrentWeek = 0;
        DateTimeOffset previousWorkoutInCurrentWeek = User.Workouts.FirstOrDefault()!.Time;

        foreach (Workout workout in User.Workouts)
        {
            if (IsWorkoutInCurrentWeek(workout))
            {
                daysWorkedOutInCurrentWeek++;
            }

            if (IsWorkoutInNextWeek(workout))
            {
                if (UserHasReachedGoal())
                {
                    currentStreak++;
                }

                if (UserHasNotReachedGoal())
                {
                    currentStreak = 0;
                }

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

    private Dictionary<MuscleGroup, decimal> GetWorkoutBuddyMuscleGroupStats()
    {
        List<Activity> activities = GetActivities();
        Dictionary<MuscleGroup, decimal> muscleGroupStats = new();

        foreach (Activity activity in activities)
        {
            Dictionary<MuscleGroup, decimal> exerciseMuscleGroupStats = activity.Exercise.MuscleGroupStats;
            foreach (KeyValuePair<MuscleGroup, decimal> muscleGroupStat in exerciseMuscleGroupStats)
            {
                if (muscleGroupStats.ContainsKey(muscleGroupStat.Key))
                {
                    muscleGroupStats[muscleGroupStat.Key] += muscleGroupStat.Value * GetPercentageOfTargetReachedInActivity(activity.Data);
                }
                else
                {
                    muscleGroupStats.Add(muscleGroupStat.Key, muscleGroupStat.Value * GetPercentageOfTargetReachedInActivity(activity.Data));
                }
            }
        }

        return muscleGroupStats;
    }
    
    private Dictionary<StrengthLevelTypes, decimal> GetBuddyOverallLevels()
    {
        List<Activity> activities = GetActivities();
        decimal powerliftingLevel = 0;
        decimal weightLiftingLevel = 0;
        decimal bodyBuildingLevel = 0;
        
        foreach (Activity activity in activities)
        {
            switch (activity.Data.Reps)
            {
                case < 5:
                    powerliftingLevel++;
                    break;
                case <= 12:
                    weightLiftingLevel++;
                    break;
                case > 12:
                    bodyBuildingLevel++;
                    break;
            }
        }

        return new()
        {
            { StrengthLevelTypes.Bodybuilding, bodyBuildingLevel },
            { StrengthLevelTypes.Powerlifting, powerliftingLevel },
            { StrengthLevelTypes.Weightlifting, weightLiftingLevel },
            { StrengthLevelTypes.Overall , powerliftingLevel + weightLiftingLevel + bodyBuildingLevel }
        };
    }
    
    

    private Dictionary<MuscleGroup, int> GetBuddyAnatomyLevel()
    {
        List<Exercise> exercises = GetExercises();
        Dictionary<MuscleGroup, int> anatomyLevels = new();
        IEnumerable<MuscleGroup> muscles = Enum.GetValues(typeof(MuscleGroup)).Cast<MuscleGroup>();

        foreach (MuscleGroup muscle in muscles)
        {
            anatomyLevels[muscle] = GetAnatomyLevel(muscle);
        }

        return anatomyLevels;

        int GetAnatomyLevel(MuscleGroup anatomyType)
        {
            return exercises.Count(exercise => exercise.MainMuscleGroup == anatomyType);
        }
    }
    
    private IEnumerable<IUserAchievement> GetBuddyAchievements(int streak, Dictionary<StrengthLevelTypes, decimal> levels)
    {
        List<int> claimedAchievements = User.ClaimedAchievements;
        List<IUserAchievement> acquiredAchievements = new();
        List<IAchievement> achievements = Achievements.AllAchievements;
        
        foreach (IAchievement achievement in achievements)
        {
            if (claimedAchievements.Contains(achievement.Id))
            {
                continue;
            }

            IUserAchievement? userAchievement = achievement switch
            {
                StreakAchievement streakAchievement => new StreakUserAchievement(streakAchievement, streak),
                WeightAchievement weightAchievement => new WeightUserAchievement(weightAchievement, GetActivities()),
                DistanceAchievement distanceAchievement => new DistanceUserAchievement(distanceAchievement, GetActivities()),
                SetsAchievement setsAchievement => new SetsUserAchievement(setsAchievement, GetActivities()),
                RepsAchievement repsAchievement => new RepsUserAchievement(repsAchievement, GetActivities()),
                LevelAchievement levelAchievement => new LevelUserAchievement(levelAchievement, levels),
                _ => null
            };

            if (userAchievement is not null)
            {
                acquiredAchievements.Add(userAchievement);
            }
        }

        return acquiredAchievements;
    }

    private List<Exercise> GetExercises()
    {
        return (from workout in User.Workouts from activity in workout.Activities select activity.Exercise).ToList();
    }

    private List<Activity> GetActivities()
    {
        return User.Workouts.SelectMany(workout => workout.Activities).ToList();
    }

    private static decimal GetPercentageOfTargetReachedInActivity(Data activityData)
    {
        switch (activityData.Type)
        {
            case ExerciseType.Cardio:
                if (activityData.Distance is not null && activityData.Duration is not null)
                {
                    decimal actual = activityData.Distance.Value * activityData.Duration.Value;
                    decimal goal = activityData.TargetDistance * activityData.TargetDuration;
                    return actual > 0 ? actual / goal : 0;
                }
                return 0;
            case ExerciseType.Strength or ExerciseType.Powerlifting or ExerciseType.OlympicWeightLifting:
                if (activityData.Weight is not null && activityData.Reps is not null && activityData.Sets is not null)
                {
                    decimal actual = activityData.Weight.Value * activityData.Reps.Value * activityData.Sets.Value;
                    decimal goal = activityData.TargetWeight / activityData.TargetReps / activityData.TargetSets;
                    return actual > 0 ? actual / goal : 0;
                }
                return 0;
            default: 
                return 0;
        }
    }
}