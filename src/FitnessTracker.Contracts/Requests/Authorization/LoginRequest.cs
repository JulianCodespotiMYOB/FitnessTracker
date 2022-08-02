namespace FitnessTracker.Contracts.Requests.Authorization;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}