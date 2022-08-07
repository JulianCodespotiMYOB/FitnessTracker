using FitnessTracker.Models.Fitness.Datas;
using FitnessTracker.Models.Fitness.Excercises;

namespace FitnessTracker.Models.Fitness.Workouts;

public class Activity
{
    public int Id { get; set; }
    public Exercise Exercise { get; set; }
    public Data Data { get; set; }
}