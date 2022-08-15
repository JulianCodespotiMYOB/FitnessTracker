using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;

namespace FitnessTracker.Interfaces.Services;

public interface IAuthorizationService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginParameters loginParameters);
    public Task<Result<RegisterResponse>> RegisterAsync(RegistrationParameters registrationParameters);
    public Task<Result<User>> GetUserAsync(int id);
    public Task<Result<IEnumerable<User>>> GetUsersAsync();
}