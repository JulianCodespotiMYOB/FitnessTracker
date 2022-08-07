using FitnessTracker.Contracts.Requests.Authorization;
using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Authorization;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Common;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IAuthorizationHandler authorizationHandler;

    public UserController(IAuthorizationHandler authorizationHandler)
    {
        this.authorizationHandler = authorizationHandler;
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

        Result<LoginResponse> loginResponse = await authorizationHandler.LoginAsync(request.Adapt<LoginParameters>());
        return !loginResponse.IsSuccess ? BadRequest(new ErrorResponse(loginResponse.Error)) : Ok(loginResponse.Value);
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
            await authorizationHandler.RegisterAsync(request.Adapt<RegistrationParameters>());
        return !registerResponse.IsSuccess
            ? BadRequest(new ErrorResponse(registerResponse.Error))
            : Ok(registerResponse.Value);
    }
}