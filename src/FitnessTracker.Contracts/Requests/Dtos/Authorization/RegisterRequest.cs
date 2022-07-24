namespace FitnessTracker.Contracts.Requests.Dtos.Authorization;

public record RegisterRequest(
string Username,
string Password,
string ConfirmPassword,
string Email,
string FirstName,
string LastName
);