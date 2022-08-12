using FitnessTracker.Models.User;

namespace FitnessTracker.Contracts.Responses.Authorization;

public class RegisterResponse
{
    public User User { get; set; }
}