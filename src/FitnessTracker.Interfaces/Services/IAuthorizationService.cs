using FitnessTracker.Contracts.Requests.Authorization;
using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services;

public interface IAuthorizationService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginRequest loginParameters);
    public Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest registrationParameters);
    public Task<Result<GetUserResponse>> GetUserAsync(int id);
    public Task<Result<GetUsersResponse>> GetUsersAsync();
}