namespace FitnessTracker.Models.Fitness.Workouts;

public class Workout
{
    public int Id { get; set; }
    public List<Activity> Activities { get; set; } = new();
    public bool Completed { get; set; } = false;
    public bool Past { get; set; } = false;
    public DateTimeOffset Time { get; set; }
}