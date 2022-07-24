namespace FitnessTracker.Models;

public record User(
    int? Id,
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName
);