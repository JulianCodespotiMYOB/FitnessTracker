namespace FitnessTracker.Contracts.Requests.Authorization;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int Age { get; set; }
    public decimal? BenchPressMax { get; set; }
    public decimal? SquatMax { get; set; }
    public decimal? DeadliftMax { get; set; }
    public string BuddyName { get; set; }
    public string BuddyDescription { get; set; }
    public int BuddyIconId { get; set; }
}