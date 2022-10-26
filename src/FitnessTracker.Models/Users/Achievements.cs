using FitnessTracker.Models.Buddy.Enums;

namespace FitnessTracker.Models.Users;

public static class Achievements
{
    public static List<IAchievement> AllAchievements { get; }

    static Achievements()
    {
        AllAchievements = new()
        {
            new StreakAchievement()
            {
                Rewards = new List<IReward>()
                {
                    new Experience()
                    {
                        Amount = 100,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 5,
                Title = "5 Day Streak",
                Description = "You've been working out for 5 days in a row! Keep it up!"
            },
            new StreakAchievement()
            {
                Rewards = new List<IReward>()
                {
                    new Experience()
                    {
                        Amount = 200,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 10,
                Title = "10 Day Streak",
                Description = "You've been working out for 10 days in a row! Keep it up!"
            },
            new StreakAchievement()
            {
                Rewards = new List<IReward>()
                {
                    new Experience()
                    {
                        Amount = 300,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 15,
                Title = "15 Day Streak",
                Description = "You've been working out for 15 days in a row! Keep it up!"
            },
            new StreakAchievement()
            {
                Rewards = new List<IReward>()
                {
                    new Experience()
                    {
                        Amount = 400,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 20,
                Title = "20 Day Streak",
                Description = "You've been working out for 20 days in a row! Keep it up!"
            },
            new StreakAchievement()
            {
                Rewards = new List<IReward>()
                {
                    new Experience()
                    {
                        Amount = 500,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 25,
                Title = "25 Day Streak",
                Description = "You've been working out for 25 days in a row! Keep it up!"
            },
        };
    }
}