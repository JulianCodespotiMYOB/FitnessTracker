namespace FitnessTracker.Models.Excercises.Excercise;

public class Excercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkoutType WorkoutType { get; }
}