using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("Users")]
public class WorkoutController : ControllerBase
{
    private readonly ILogger logger;
    private readonly IWorkoutService workoutService;

    public WorkoutController(ILogger<WorkoutController> logger, IWorkoutService workoutService)
    {
        this.logger = logger;
        this.workoutService = workoutService;
    }

    [HttpPost("{userId}/Workouts")]
    public async Task<IActionResult> RecordWorkout(
        [FromBody] RecordWorkoutRequest request,
        int userId,
        [FromServices] IValidator<RecordWorkoutRequest> validator
    )
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            logger.LogError($"Invalid request: {validationResult}");
            return BadRequest(validationResult.Errors);
        }
        var recordWorkoutResponse = await workoutService.RecordWorkout(request, userId);
        return Ok(recordWorkoutResponse.Value);
    }
    
    [HttpGet("{userId}/Workout")]
    public async Task<IActionResult> GetWorkout(
        int userId,
        int workoutId
    )
    {
        var getWorkoutResponse = await workoutService.GetWorkout(workoutId, userId);
        return Ok(getWorkoutResponse.Value);
    }
    
    [HttpGet("{userId}/Workouts")]
    public async Task<IActionResult> GetWorkouts(
        int userId
    )
    {
        var getWorkoutsResponse = await workoutService.GetWorkouts(userId);
        return Ok(getWorkoutsResponse.Value);
    }
}