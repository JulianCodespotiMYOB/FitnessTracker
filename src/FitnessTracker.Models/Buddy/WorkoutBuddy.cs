﻿using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Buddy.BuddyAnatomy;
using FitnessTracker.Models.Fitness.Excercises;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Muscles;

namespace FitnessTracker.Models.Buddy;

public class WorkoutBuddy
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int IconId { get; set; }
    public BuddyData Data => GetWorkoutBuddyData();

    private BuddyData GetWorkoutBuddyData()
    {
        BuddyData buddyData = new();
        buddyData.Streak = GetWorkoutBuddyStreak();
        buddyData.Strength = GetWorkoutBuddyStrength();
        buddyData.Speed = GetWorkoutBuddySpeed();

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

        int currentStreak = 0;
        int daysWorkedOutInCurrentWeek = 0;
        DateTimeOffset lastWorkoutDateOfTheWeek = User.Workouts.FirstOrDefault()!.Time;

        foreach (Workout workout in User.Workouts)
        {
            if (workout.Time < lastWorkoutDateOfTheWeek.AddDays(7)) daysWorkedOutInCurrentWeek++;

            if (workout.Time >= lastWorkoutDateOfTheWeek.AddDays(7))
            {
                if (daysWorkedOutInCurrentWeek >= User.WeeklyWorkoutAmountGoal) currentStreak++;
                if (daysWorkedOutInCurrentWeek < User.WeeklyWorkoutAmountGoal) currentStreak = 0;
                daysWorkedOutInCurrentWeek = 0;
                lastWorkoutDateOfTheWeek = lastWorkoutDateOfTheWeek.AddDays(7);
            }
        }

        return currentStreak;
    }

    private double GetWorkoutBuddyStrength()
    {
        List<Activity> activities = GetActivities();
        return activities.Where(activity => activity.Data.Type == ExerciseType.Strength)
            .Aggregate<Activity, double>(0, (current, activity) => current + 1);
    }

    private double GetWorkoutBuddySpeed()
    {
        List<Activity> activities = GetActivities();
        double workoutBuddySpeed = 0;
        foreach (Activity activity in activities)
            if (activity.Data.Type == ExerciseType.Cardio)
                workoutBuddySpeed += 1;
        return workoutBuddySpeed;
    }

    private void SetBuddyAnatomyLevel(BuddyData buddyData)
    {
        List<Exercise> exercises = GetExercises();
        foreach (IBuddyAnatomy buddyAnatomy in buddyData.Anatomy)
            buddyAnatomy.Level = GetAnatomyLevel(buddyAnatomy.MuscleGroup);

        int GetAnatomyLevel(MuscleGroup anatomyType)
        {
            return exercises.Count(exercise => exercise.PrimaryMuscleGroup == anatomyType);
        }
    }
}