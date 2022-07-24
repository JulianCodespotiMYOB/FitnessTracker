using FitnessTracker.Contracts.Requests.Dtos.Authorization;
using FitnessTracker.Interfaces;
using FitnessTracker.Models;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;
namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserAuthenticationController : ControllerBase
{
    private readonly ILogger logger;
    private readonly IAuthorizationHandler authorizationHandler;

    public UserAuthenticationController(ILogger<UserAuthenticationController> logger, IAuthorizationHandler authorizationHandler)
    {
        this.logger = logger;
        this.authorizationHandler = authorizationHandler;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, [FromServices] IValidator<LoginRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var user = await authorizationHandler.LoginAsync(request.Adapt<LoginParameters>());
        if (!user.IsSuccess)
        {
            return BadRequest(user.Error);
        }
        return Ok(user.Value.Adapt<User>());
    }
}
