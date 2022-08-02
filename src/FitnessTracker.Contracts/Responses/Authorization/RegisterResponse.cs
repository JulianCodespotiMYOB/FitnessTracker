using FitnessTracker.Models.Authorization;

namespace FitnessTracker.Contracts.Responses.Authorization;

public class RegisterResponse
{
    public User User { get; set; }
}