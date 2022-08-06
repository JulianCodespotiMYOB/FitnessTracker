namespace FitnessTracker.Models.WorkoutBuddy;

public class WorkoutBuddy
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int IconId { get; set; }
    public BuddyData Data { get; set; } = new();
}