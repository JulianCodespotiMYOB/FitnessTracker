namespace FitnessTracker.Models.Excercises.Workout;

public class Workout
{
    public int Id { get; set; }
    public double Time { get; set; }
    public List<Activity> Activities { get; set; }
    public bool Completed { get; set; }
}