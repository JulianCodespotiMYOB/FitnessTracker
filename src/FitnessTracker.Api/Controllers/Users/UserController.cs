using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Interfaces.Services.Authorization;
using FitnessTracker.Models.Common;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Users;

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
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<LoginResponse> loginResponse = await _authorizationService.LoginAsync(request.Adapt<LoginRequest>());
        return !loginResponse.IsSuccess
            ? BadRequest(new ErrorResponse(loginResponse.Error))
            : Ok(loginResponse.Value);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request,
        [FromServices] IValidator<RegisterRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<RegisterResponse> registerResponse =
            await _authorizationService.RegisterAsync(request.Adapt<RegisterRequest>());
        return !registerResponse.IsSuccess
            ? BadRequest(new ErrorResponse(registerResponse.Error))
            : Ok(registerResponse.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        Result<GetUserResponse> user = await _authorizationService.GetUserAsync(id);
        return !user.IsSuccess
            ? BadRequest(new ErrorResponse(user.Error))
            : Ok(user.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        Result<GetUsersResponse> users = await _authorizationService.GetUsersAsync();
        return !users.IsSuccess
            ? BadRequest(new ErrorResponse(users.Error))
            : Ok(users.Value);
    }
}