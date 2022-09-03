using FitnessTracker.Models.Users;

namespace FitnessTracker.Contracts.Responses.Users;

public record GetUsersResponse(IEnumerable<User> Users);