using AutoBogus;
using FitnessTracker.Models.Fitness.Workouts;
using FitnessTracker.Models.Users;
using Xunit;

namespace FitnessTracker.UnitTests.Buddy;

public class WorkoutBuddyTest
{
    [Fact]
    public void GivenAUserHasWorkedOut2WeeksInARowAndWeeklyGoalIs1_WhenCalculatingBuddyStreak_ThenStreakIs2()
    {
        // Given
        User user = new AutoFaker<User>().Generate();
        user.WeeklyWorkoutAmountGoal = 1;
        List<Workout>? workouts = new AutoFaker<Workout>().Generate(3);

        for (int i = 0; i < 3; i++) workouts[i].Time = DateTime.Now.AddDays(6 * i);

        user.Workouts = workouts;
        user.WorkoutBuddy.User = user;

        // When
        int streak = user.WorkoutBuddy.Data.Streak;

        // Then
        Assert.Equal(2, streak);
    }
}