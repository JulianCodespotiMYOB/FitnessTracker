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
    public User User { get; set; }
    public string Name { get; set; }
    public BuddyData Data => GetWorkoutBuddyData();

    private BuddyData GetWorkoutBuddyData()
    {
        BuddyData buddyData = new();
        SetWorkoutBuddyStreak(buddyData);
        SetWorkoutBuddyMuscleGroupStats(buddyData);
        SetBuddyOverallLevels(buddyData);
        SetBuddyAnatomyLevel(buddyData);
        SetBuddyAchievements(buddyData);

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

    private void SetWorkoutBuddyStreak(BuddyData buddyData)
    {
        if (User.Workouts.Count <= User.WeeklyWorkoutAmountGoal)
        {
            return;
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

        buddyData.Streak = currentStreak;

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

    private void SetWorkoutBuddyMuscleGroupStats(BuddyData buddyData)
    {
        List<Activity> activities = GetActivities();
        Dictionary<MuscleGroup, double> muscleGroupStats = new();
        foreach (Activity activity in activities)
        {
            Dictionary<MuscleGroup, double> exerciseMuscleGroupStats = activity.Exercise.MuscleGroupStats;
            foreach (KeyValuePair<MuscleGroup, double> muscleGroupStat in exerciseMuscleGroupStats)
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
        buddyData.MuscleGroupStats = muscleGroupStats;
    }

    private double GetPercentageOfTargetReachedInActivity(Data activityData)
    {
        switch (activityData.Type)
        {
            case ExerciseType.Cardio:
                if (activityData.Distance is not null)
                {
                    return (activityData.Distance / activityData.TargetDistance).Value;
                }
                return 0;
            case ExerciseType.Strength or ExerciseType.Powerlifting or ExerciseType.OlympicWeightLifting:
                if (activityData.Weight is not null && activityData.Reps is not null && activityData.Sets is not null)
                {
                    return (activityData.Weight * activityData.Reps * activityData.Sets / activityData.TargetWeight / activityData.TargetReps / activityData.TargetSets).Value;
                }
                return 0;
            default: return 0;
        }
    }
    
    private void SetBuddyOverallLevels(BuddyData buddyData)
    {
        List<Activity> activities = GetActivities();
        double powerliftingLevel = 0;
        double weightLiftingLevel = 0;
        double bodyBuildingLevel = 0;
        
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
        buddyData.LevelStats.Add(StrengthLevelTypes.Bodybuilding, bodyBuildingLevel);
        buddyData.LevelStats.Add(StrengthLevelTypes.Powerlifting, powerliftingLevel);
        buddyData.LevelStats.Add(StrengthLevelTypes.Weightlifting, weightLiftingLevel);
        buddyData.LevelStats.Add(StrengthLevelTypes.Overall, powerliftingLevel + weightLiftingLevel + bodyBuildingLevel);
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
    
    private void SetBuddyAchievements(BuddyData buddyData)
    {
        List<Achievement> acquiredAchievements = new();
        List<Achievement> achievements = Achievements.AllAchievements;
        
        foreach (Achievement achievement in achievements)
        {
            bool isEligibleForAchievement = achievement switch
            {
                StreakAchievement streakAchievement => IsEligibleForStreakAchievement(streakAchievement, buddyData.Streak),
                WeightAchievement weightAchievement => IsEligibleForWeightAchievement(weightAchievement),
                DistanceAchievement distanceAchievement => IsEligibleForDistanceAchievement(distanceAchievement),
                SetsAchievement setsAchievement => IsEligibleForSetsAchievement(setsAchievement),
                RepsAchievement repsAchievement => IsEligibleForRepsAchievement(repsAchievement),
                LevelAchievement levelAchievement => IsEligibleForLevelAchievement(levelAchievement, buddyData.LevelStats),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (isEligibleForAchievement)
            {
                acquiredAchievements.Add(achievement);
            }
        }

        buddyData.Achievements = acquiredAchievements;
    }
    
    private bool IsEligibleForStreakAchievement(StreakAchievement achievement, int streak)
    {
        if (streak >= achievement.TargetStreak)
        {
            return true;
        }
        return false;
    }
    
    private bool IsEligibleForWeightAchievement(WeightAchievement achievement)
    {
        List<Activity> activities = GetActivities();
        foreach (Activity activity in activities)
        {
            if (achievement.HasTargetMuscleGroup && activity.Exercise.MainMuscleGroup == achievement.TargetMuscleGroup)
            {
                if (activity.Data.Weight >= achievement.TargetWeight)
                {
                    return true;
                }
            }
            else if (activity.Data.Weight >= achievement.TargetWeight)
            {
                return true;
            }
        }
        return false;
    }
    
    private bool IsEligibleForLevelAchievement(LevelAchievement achievement, Dictionary<StrengthLevelTypes, double> levelStats)
    {
        if (levelStats[achievement.TargetStrengthLevelType] >= achievement.TargetLevel)
        {
            return true;
        }
        return false;
    }
    
    private bool IsEligibleForDistanceAchievement(DistanceAchievement achievement)
    {
        List<Activity> activities = GetActivities();
        foreach (Activity activity in activities)
        {
            if (activity.Data.Distance >= achievement.TargetDistance)
            {
                return true;
            }
        }
        return false;
    }
    
    private bool IsEligibleForSetsAchievement(SetsAchievement achievement)
    {
        List<Activity> activities = GetActivities();
        foreach (Activity activity in activities)
        {
            if (achievement.HasTargetMuscleGroup && activity.Exercise.MainMuscleGroup == achievement.TargetMuscleGroup)
            {
                if (activity.Data.Sets >= achievement.TargetSets)
                {
                    return true;
                }
            }
            else if (activity.Data.Sets >= achievement.TargetSets)
            {
                return true;
            }
        }
        return false;
    }
    
    private bool IsEligibleForRepsAchievement(RepsAchievement achievement)
    {
        List<Activity> activities = GetActivities();
        foreach (Activity activity in activities)
        {
            if (achievement.HasTargetMuscleGroup && activity.Exercise.MainMuscleGroup == achievement.TargetMuscleGroup)
            {
                if (activity.Data.Reps >= achievement.TargetReps)
                {
                    return true;
                }
            }
            else if (activity.Data.Reps >= achievement.TargetReps)
            {
                return true;
            }
        }
        return false;
    }
}