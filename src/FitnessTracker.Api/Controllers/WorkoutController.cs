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
        if (recordWorkoutResponse.IsSuccess is false) return BadRequest(recordWorkoutResponse.Error);
        return Ok(recordWorkoutResponse.Value);
    }

    [HttpGet("{userId}/Workout")]
    public async Task<IActionResult> GetWorkout(
        int userId,
        int workoutId
    )
    {
        var getWorkoutResponse = await workoutService.GetWorkout(workoutId, userId);
        if (getWorkoutResponse.IsSuccess is false) return BadRequest(getWorkoutResponse.Error);

        return Ok(getWorkoutResponse.Value);
    }

    [HttpGet("{userId}/Workouts")]
    public async Task<IActionResult> GetWorkouts(
        int userId
    )
    {
        var getWorkoutsResponse = await workoutService.GetWorkouts(userId);
        if (getWorkoutsResponse.IsSuccess is false) return BadRequest(getWorkoutsResponse.Error);
        return Ok(getWorkoutsResponse.Value);
    }

    [HttpPut("{userId}/Workouts/{workoutId}")]
    public async Task<IActionResult> UpdateWorkout(
        int userId,
        int workoutId,
        [FromBody] UpdateWorkoutRequest request,
        [FromServices] IValidator<UpdateWorkoutRequest> validator
    )
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            logger.LogError($"Invalid request: {validationResult}");
            return BadRequest(validationResult.Errors);
        }

        var updateWorkoutResponse = await workoutService.UpdateWorkout(request, workoutId, userId);
        if (updateWorkoutResponse.IsSuccess is false) return BadRequest(updateWorkoutResponse.Error);
        return Ok(updateWorkoutResponse.Value);
    }

    [HttpDelete("{userId}/Workouts/{workoutId}")]
    public async Task<IActionResult> DeleteWorkout(
        int userId,
        int workoutId
    )
    {
        var deleteWorkoutResponse = await workoutService.DeleteWorkout(workoutId, userId);
        if (deleteWorkoutResponse.IsSuccess is false) return BadRequest(deleteWorkoutResponse.Error);
        return Ok(deleteWorkoutResponse.Value);
    }
}