namespace FitnessTracker.Models.Authorization;

public record RegistrationParameters(
    string Username,
    string Password,
    string ConfirmPassword,
    string Email,
    string FirstName,
    string LastName,
    string Name,
    string Description,
    string IconUrl
);