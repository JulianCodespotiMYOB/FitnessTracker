namespace FitnessTracker.Models.Users;

public class Achievement
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Progress { get; set; }
    public int Total { get; set; }
    public List<Reward> Rewards { get; set; } = new();
}

public abstract class Reward
{
    public int Id { get; set; }
}

public class Title : Reward
{
    public string Name { get; set; }
}