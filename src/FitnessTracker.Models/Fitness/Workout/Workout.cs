namespace FitnessTracker.Models.Fitness.Workout;

public class Workout
{
    public int Id { get; set; }
    public List<Activity> Activities { get; set; } = new();
    public bool Completed { get; set; } = false;
}