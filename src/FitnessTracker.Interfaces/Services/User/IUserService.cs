using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.User;

public interface IUserService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginRequest loginParameters);
    public Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest registrationParameters);
    public Task<Result<GetUserResponse>> GetUserAsync(int id);
    public Task<Result<GetUsersResponse>> GetUsersAsync();
    public Task<Result<UpdateSettingsResponse>> SetSettingsAsync(int id, UpdateSettingsRequest request);
}