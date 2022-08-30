using FitnessTracker.Contracts.Requests.Workouts;
using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Workouts;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("Users")]
public class WorkoutNamesController : ControllerBase
{
    private readonly IWorkoutNamesService _workoutNamesService;

    public WorkoutNamesController(IWorkoutNamesService workoutNamesService)
    {
        _workoutNamesService = workoutNamesService;
    }

    [HttpGet("{userId:int}/WorkoutsNames")]
    public async Task<IActionResult> GetWorkoutNames(
        [FromRoute] int userId,
        [FromQuery] GetWorkoutNamesRequest request
    )
    {
        Result<GetWorkoutNamesResponse> getWorkoutNamesResponse = await _workoutNamesService.GetWorkoutNames(userId, request);
        return getWorkoutNamesResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getWorkoutNamesResponse.Error))
            : Ok(getWorkoutNamesResponse.Value);
    }
}