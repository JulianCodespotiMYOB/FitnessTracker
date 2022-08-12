using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.User.Parameters;

namespace FitnessTracker.Interfaces.Services;

public interface IAuthorizationService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginParameters loginParameters);
    public Task<Result<RegisterResponse>> RegisterAsync(RegistrationParameters registrationParameters);
}