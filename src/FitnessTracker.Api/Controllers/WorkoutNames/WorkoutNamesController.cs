using FitnessTracker.Contracts.Requests.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Interfaces.Services.WorkoutNames;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.WorkoutNames;

[ApiController]
[Route("Users")]
public class WorkoutNamesController : ControllerBase
{
    private readonly IWorkoutNamesService _workoutNamesService;

    public WorkoutNamesController(IWorkoutNamesService workoutNamesService)
    {
        _workoutNamesService = workoutNamesService;
    }

    [HttpGet("{userId:int}/WorkoutNames")]
    public async Task<IActionResult> GetWorkoutNames(
        [FromRoute] int userId,
        [FromQuery] GetWorkoutNamesRequest request
    )
    {
        Result<GetWorkoutNamesResponse> getWorkoutNamesResponse =
            await _workoutNamesService.GetWorkoutNames(userId, request);
        return getWorkoutNamesResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getWorkoutNamesResponse.Error))
            : Ok(getWorkoutNamesResponse.Value);
    }
}