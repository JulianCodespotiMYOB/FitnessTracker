using System;
using Xunit;

namespace FitnessTracker.Models.Test
{
    public class Tests
    {
        [Fact]
        public void GivenUserWorksOutFor3Weeks_WhenCalculatingStreak_ThenStreakWillBe3()
        {
            // Arrange
            var user = new User();
            user.WorkoutHistory.Add(new Workout { Date = DateTime.Now.AddDays(-3) });
            user.WorkoutHistory.Add(new Workout { Date = DateTime.Now.AddDays(-2) });
            user.WorkoutHistory.Add(new Workout { Date = DateTime.Now.AddDays(-1) });
            user.WorkoutHistory.Add(new Workout { Date = DateTime.Now });
            // Act
            var streak = user.GetStreak();
            // Assert
            Assert.Equal(3, streak);
        }
    }
}