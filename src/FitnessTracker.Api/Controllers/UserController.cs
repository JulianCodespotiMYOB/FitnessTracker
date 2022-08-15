using FitnessTracker.Contracts.Requests.Authorization;
using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public UserController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request,
        [FromServices] IValidator<LoginRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));

        Result<LoginResponse> loginResponse = await _authorizationService.LoginAsync(request.Adapt<LoginParameters>());
        return !loginResponse.IsSuccess ? BadRequest(new ErrorResponse(loginResponse.Error)) : Ok(loginResponse.Value);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request,
        [FromServices] IValidator<RegisterRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));

        Result<RegisterResponse> registerResponse =
            await _authorizationService.RegisterAsync(request.Adapt<RegistrationParameters>());
        return !registerResponse.IsSuccess
            ? BadRequest(new ErrorResponse(registerResponse.Error))
            : Ok(registerResponse.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        Result<User> user = await _authorizationService.GetUserAsync(id);
        return !user.IsSuccess
            ? BadRequest(new ErrorResponse(user.Error))
            : Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        Result<IEnumerable<User>> users = await _authorizationService.GetUsersAsync();
        return !users.IsSuccess
            ? BadRequest(new ErrorResponse(users.Error))
            : Ok(users);
    }
}