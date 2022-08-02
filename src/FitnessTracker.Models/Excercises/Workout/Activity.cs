namespace FitnessTracker.Models.Excercises.Workout;

public class Activity
{
    public int Id { get; set; }
    public Excercise.Excercise Excercise { get; set; }
    public Data.Data Data { get; set; }
}