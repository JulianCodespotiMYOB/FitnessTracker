using FitnessTracker.Contracts.Requests.Authorization;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserAuthenticationController : ControllerBase
{
    private readonly IAuthorizationHandler authorizationHandler;
    private readonly ILogger logger;

    public UserAuthenticationController(ILogger<UserAuthenticationController> logger,
        IAuthorizationHandler authorizationHandler)
    {
        this.logger = logger;
        this.authorizationHandler = authorizationHandler;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request,
        [FromServices] IValidator<LoginRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var loginResponse = await authorizationHandler.LoginAsync(request.Adapt<LoginParameters>());
        if (!loginResponse.IsSuccess) return BadRequest(loginResponse.Error);

        return Ok(loginResponse.Value);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request,
        [FromServices] IValidator<RegisterRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            logger.LogError(validationResult.Errors.ToString());
            return BadRequest(validationResult.Errors);
        }

        var registerResponse = await authorizationHandler.RegisterAsync(request.Adapt<RegistrationParameters>());
        if (!registerResponse.IsSuccess)
        {
            logger.LogError(registerResponse.Error);
            return BadRequest(registerResponse.Error);
        }

        return Ok(registerResponse.Value);
    }
}