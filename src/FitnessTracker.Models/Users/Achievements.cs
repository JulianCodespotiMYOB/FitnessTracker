using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Fitness.Enums;

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
                Id = 0,
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Id = 0,
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
                Id = 1,
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Id = 1,
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
                Id = 2,
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Id = 2,
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
                Id = 3,
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Id = 3,
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
                Id = 4,
                Rewards = new List<Reward>()
                {
                    new Experience()
                    {
                        Id = 4,
                        Amount = 500,
                        StrengthLevel = StrengthLevelTypes.Bodybuilding
                    }
                },
                TargetStreak = 25,
                Title = "25 Day Streak",
                Description = "You've been working out for 25 days in a row! Keep it up!"
            },
            new WeightAchievement()
            {
                Id = 5,
                Rewards = new List<Reward>()
                {
                    new Title()
                    {
                        Id = 5,
                        Name = "Iron Chest"
                    }
                },
                TargetWeight = 100,
                TargetMuscleGroup = MuscleGroup.Chest,
                Title = "100 lbs bench press",
                Description = "You've lifted 100 lbs! Keep it up!"
            },
        };
    }
}