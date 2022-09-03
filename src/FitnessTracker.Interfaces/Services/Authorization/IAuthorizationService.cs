using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.Authorization;

public interface IAuthorizationService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginRequest loginParameters);
    public Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest registrationParameters);
    public Task<Result<GetUserResponse>> GetUserAsync(int id);
    public Task<Result<GetUsersResponse>> GetUsersAsync();
}