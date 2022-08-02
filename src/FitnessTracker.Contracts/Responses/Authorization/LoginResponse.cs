using FitnessTracker.Models.Authorization;

namespace FitnessTracker.Contracts.Responses.Authorization;

public class LoginResponse
{
    public User User { get; set; }
}