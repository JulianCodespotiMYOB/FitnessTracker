using FitnessTracker.Models.Fitness.Datas;
using FitnessTracker.Models.Fitness.Excercises;

namespace FitnessTracker.Models.Fitness.Workout;

public class Activity
{
    public int Id { get; set; }
    public Excercise Excercise { get; set; }
    public Data Data { get; set; }
}