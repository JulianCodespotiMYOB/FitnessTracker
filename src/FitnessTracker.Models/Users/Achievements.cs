using FitnessTracker.Models.Buddy.Enums;

namespace FitnessTracker.Models.Users;

public static class Achievements
{
    public static List<Achievement> AllAchievements { get; }

    static Achievements()
    {
        AllAchievements = new()
        {
            new StreakAchievement()
            {
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Amount = 100,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 5,
                Title = "5 Day Streak"
            },
            new StreakAchievement()
            {
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Amount = 200,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 10,
                Title = "10 Day Streak"
            },
            new StreakAchievement()
            {
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Amount = 300,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 15,
                Title = "15 Day Streak"
            },
            new StreakAchievement()
            {
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Amount = 400,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 20,
                Title = "20 Day Streak"
            },
            new StreakAchievement()
            {
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Amount = 500,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 25,
                Title = "25 Day Streak"
            },
        };
    }
}