using FitnessTracker.Models.Fitness.Datas;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Fitness.Exercises;

namespace FitnessTracker.Models.Fitness.Workouts;

public class Activity
{
    public int Id { get; set; }
    public Exercise Exercise { get; set; }
    public Data Data { get; set; }
}