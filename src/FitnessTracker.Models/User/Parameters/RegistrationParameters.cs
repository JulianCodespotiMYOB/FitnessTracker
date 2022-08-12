namespace FitnessTracker.Models.User.Parameters;

public record RegistrationParameters(
    string Username,
    string Password,
    string ConfirmPassword,
    string Email,
    string FirstName,
    string LastName,
    decimal Height,
    decimal Weight,
    int Age,
    decimal? BenchPressMax,
    decimal? SquatMax,
    decimal? DeadliftMax,
    string BuddyName,
    string BuddyDescription,
    int BuddyIconId
);