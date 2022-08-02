using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly ILogger logger;
    private readonly IWorkoutService workoutService;

    public WorkoutController(ILogger<WorkoutController> logger, IWorkoutService workoutService)
    {
        this.logger = logger;
        this.workoutService = workoutService;
    }

    [HttpPost("Record")]
    public async Task<IActionResult> RecordWorkout(
        [FromBody] RecordWorkoutRequest request,
        [FromServices] IValidator<RecordWorkoutRequest> validator
    )
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            logger.LogError($"Invalid request: {validationResult.ToString()}");
            return BadRequest(validationResult.Errors);
        }

        var recordWorkoutResponse = await workoutService.RecordWorkout(request);
        return Ok(recordWorkoutResponse.Value);
    }
}