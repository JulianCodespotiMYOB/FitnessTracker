using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Interfaces.Services.User;
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
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request,
        [FromServices] IValidator<LoginRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<LoginResponse> loginResponse = await _userService.LoginAsync(request);
        return !loginResponse.IsSuccess
            ? BadRequest(new ErrorResponse(loginResponse.Error))
            : Ok(loginResponse.Value);
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(RegisterResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request,
        [FromServices] IValidator<RegisterRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<RegisterResponse> registerResponse = await _userService.RegisterAsync(request);
        return !registerResponse.IsSuccess
            ? BadRequest(new ErrorResponse(registerResponse.Error))
            : Ok(registerResponse.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetUser(int id)
    {
        Result<GetUserResponse> user = await _userService.GetUserAsync(id);
        return !user.IsSuccess
            ? BadRequest(new ErrorResponse(user.Error))
            : Ok(user.Value);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUsersResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetUsers()
    {
        Result<GetUsersResponse> users = await _userService.GetUsersAsync();
        return !users.IsSuccess
            ? BadRequest(new ErrorResponse(users.Error))
            : Ok(users.Value);
    }

    [HttpPut("{id}/Settings")]
    [ProducesResponseType(typeof(UpdateSettingsResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> UpdateSettings(int id, [FromBody] UpdateSettingsRequest request)
    {
        Result<UpdateSettingsResponse> setSettingsResponse = await _userService.SetSettingsAsync(id, request);
        return !setSettingsResponse.IsSuccess
            ? BadRequest(new ErrorResponse(setSettingsResponse.Error))
            : Ok(setSettingsResponse.Value);
    }
}