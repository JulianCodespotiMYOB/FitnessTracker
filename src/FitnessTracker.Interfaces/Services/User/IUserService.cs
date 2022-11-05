using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces.Services.User;

public interface IUserService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginRequest loginParameters);
    public Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest registrationParameters);
    public Task<Result<GetUserResponse>> GetUserAsync(int id);
    public Task<GetUsersResponse> GetUsersAsync();
    public Task<Result<UpdateUserResponse>> UpdateUserAsync(int id, UpdateUserRequest request);
    public Task<Result<RecordAchievementResponse>> RecordAchievementAsync(int id, int achievementId);
}