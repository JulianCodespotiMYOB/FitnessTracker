using FitnessTracker.Contracts.Requests.Workouts.GetWorkouts;
using FitnessTracker.Contracts.Requests.Workouts.RecordWorkout;
using FitnessTracker.Contracts.Requests.Workouts.UpdateWorkout;
using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.Workouts.DeleteWorkout;
using FitnessTracker.Contracts.Responses.Workouts.GetWorkouts;
using FitnessTracker.Contracts.Responses.Workouts.RecordWorkout;
using FitnessTracker.Contracts.Responses.Workouts.UpdateWorkout;
using FitnessTracker.Interfaces.Services.Workouts;
using FitnessTracker.Models.Common;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Workouts;

[ApiController]
[Route("Users")]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpPost("{userId:int}/Workouts")]
    [ProducesResponseType(typeof(RecordWorkoutResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> RecordWorkout(
        [FromBody] RecordWorkoutRequest request,
        [FromRoute] int userId,
        [FromServices] IValidator<RecordWorkoutRequest> validator
    )
    {
        Result<RecordWorkoutResponse> recordWorkoutResponse = await _workoutService.RecordWorkout(request, userId);
        return recordWorkoutResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(recordWorkoutResponse.Error))
            : Ok(recordWorkoutResponse.Value);
    }

    [HttpGet("{userId:int}/Workouts/{workoutId:int}")]
    [ProducesResponseType(typeof(GetWorkoutResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetWorkout(
        [FromRoute] int userId,
        [FromRoute] int workoutId
    )
    {
        Result<GetWorkoutResponse> getWorkoutResponse = await _workoutService.GetWorkout(workoutId, userId);
        return getWorkoutResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getWorkoutResponse.Error))
            : Ok(getWorkoutResponse.Value);
    }

    [HttpGet("{userId:int}/Workouts")]
    [ProducesResponseType(typeof(GetWorkoutsResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetWorkouts(
        [FromRoute] int userId,
        [FromQuery] GetWorkoutsRequest request
    )
    {
        Result<GetWorkoutsResponse> getWorkoutsResponse = await _workoutService.GetWorkouts(userId, request);
        return getWorkoutsResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getWorkoutsResponse.Error))
            : Ok(getWorkoutsResponse.Value);
    }

    [HttpPut("{userId:int}/Workouts/{workoutId:int}")]
    [ProducesResponseType(typeof(UpdateWorkoutResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> UpdateWorkout(
        [FromRoute] int userId,
        [FromRoute] int workoutId,
        [FromBody] UpdateWorkoutRequest request,
        [FromServices] IValidator<UpdateWorkoutRequest> validator
    )
    {
        Result<UpdateWorkoutResponse> updateWorkoutResponse = await _workoutService.UpdateWorkout(request, workoutId, userId);
        return updateWorkoutResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(updateWorkoutResponse.Error))
            : Ok(updateWorkoutResponse.Value);
    }

    [HttpDelete("{userId:int}/Workouts/{workoutId:int}")]
    [ProducesResponseType(typeof(DeleteWorkoutResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> DeleteWorkout(
        [FromRoute] int userId,
        [FromRoute] int workoutId
    )
    {
        Result<DeleteWorkoutResponse> deleteWorkoutResponse = await _workoutService.DeleteWorkout(workoutId, userId);
        return deleteWorkoutResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(deleteWorkoutResponse.Error))
            : Ok(deleteWorkoutResponse.Value);
    }
}