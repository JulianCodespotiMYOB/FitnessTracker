using FitnessTracker.Contracts.Requests.Dtos.Authorization;
using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Models;
using FitnessTracker.Models.Common;

namespace FitnessTracker.Interfaces
{
  public interface IAuthorizationHandler
  {
    public Task<Result<User>> LoginAsync(LoginParameters loginParameters);
    public Task<Result<User>> RegisterAsync(RegistrationParameters registrationParameters);
  }
}