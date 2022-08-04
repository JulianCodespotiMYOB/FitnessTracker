namespace FitnessTracker.Models.Fitness.Workout;

public class Workout
{
    public int Id { get; set; }
    public List<Activity> Activities { get; set; }
    public bool Completed { get; set; }
}