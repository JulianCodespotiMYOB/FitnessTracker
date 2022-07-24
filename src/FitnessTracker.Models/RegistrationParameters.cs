namespace FitnessTracker.Models;

public record RegistrationParameters(
    string Username,
    string Password,
    string ConfirmPassword,
    string Email,
    string FirstName,
    string LastName
);