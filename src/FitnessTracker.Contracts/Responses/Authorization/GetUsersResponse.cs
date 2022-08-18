using FitnessTracker.Models.Users;

namespace FitnessTracker.Contracts.Responses.Authorization;

public record GetUsersResponse(IEnumerable<User> Users);