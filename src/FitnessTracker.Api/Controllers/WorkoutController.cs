using FitnessTracker.Contracts.Requests.Workout;
using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Workout;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Common;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("Users")]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        this.workoutService = workoutService;
    }

    [HttpPost("{userId}/Workouts")]
    public async Task<IActionResult> RecordWorkout(
        [FromBody] RecordWorkoutRequest request,
        int userId,
        [FromServices] IValidator<RecordWorkoutRequest> validator
    )
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<RecordWorkoutResponse> recordWorkoutResponse = await workoutService.RecordWorkout(request, userId);
        if (recordWorkoutResponse.IsSuccess is false)
        {
            return BadRequest(new ErrorResponse(recordWorkoutResponse.Error));
        }
        return Ok(recordWorkoutResponse.Value);
    }
    
    [HttpGet("{userId}/Workout")]
    public async Task<IActionResult> GetWorkout(
        int userId,
        int workoutId
    )
    {
        Result<GetWorkoutResponse> getWorkoutResponse = await workoutService.GetWorkout(workoutId, userId);
        if (getWorkoutResponse.IsSuccess is false)
        {
            return BadRequest(new ErrorResponse(getWorkoutResponse.Error));
        }
        
        return Ok(getWorkoutResponse.Value);
    }
    
    [HttpGet("{userId}/Workouts")]
    public async Task<IActionResult> GetWorkouts(
        int userId
    )
    {
        Result<GetWorkoutsResponse> getWorkoutsResponse = await workoutService.GetWorkouts(userId);
        if (getWorkoutsResponse.IsSuccess is false)
        {
            return BadRequest(new ErrorResponse(getWorkoutsResponse.Error));
        }

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
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<UpdateWorkoutResponse> updateWorkoutResponse = await workoutService.UpdateWorkout(request, workoutId, userId);
        if (updateWorkoutResponse.IsSuccess is false)
        {
            return BadRequest(new ErrorResponse(updateWorkoutResponse.Error));
        }

        return Ok(updateWorkoutResponse.Value);
    }
    
    [HttpDelete("{userId}/Workouts/{workoutId}")]
    public async Task<IActionResult> DeleteWorkout(
        int userId,
        int workoutId
    )
    {
        Result<DeleteWorkoutResponse> deleteWorkoutResponse = await workoutService.DeleteWorkout(workoutId, userId);
        if (deleteWorkoutResponse.IsSuccess is false)
        {
            return BadRequest(new ErrorResponse(deleteWorkoutResponse.Error));
        }

        return Ok(deleteWorkoutResponse.Value);
    }
}