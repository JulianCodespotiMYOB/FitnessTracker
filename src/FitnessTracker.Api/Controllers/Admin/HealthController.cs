using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("Health")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<IActionResult> GetHealthAsync()
    {
        HealthReport healthReport = await _healthCheckService.CheckHealthAsync();
        
        return healthReport.Status == HealthStatus.Healthy
            ? Ok(healthReport)
            : BadRequest(healthReport);
    }
}