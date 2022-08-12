using FitnessTracker.Models.User;

namespace FitnessTracker.Contracts.Responses.Authorization;

public class LoginResponse
{
    public User User { get; set; }
}