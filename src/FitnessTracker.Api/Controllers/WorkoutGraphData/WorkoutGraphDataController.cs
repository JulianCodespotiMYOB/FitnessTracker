using FitnessTracker.Contracts.Requests.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Interfaces.Services.WorkoutGraphData;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.WorkoutGraphData;

[ApiController]
[Route("Users")]
public class WorkoutGraphDataController : ControllerBase
{
    private readonly IWorkoutGraphDataService _workoutGraphDataService;

    public WorkoutGraphDataController(IWorkoutGraphDataService workoutGraphDataService)
    {
        _workoutGraphDataService = workoutGraphDataService;
    }

    [HttpGet("{userId:int}/WorkoutGraphData")]
    public async Task<IActionResult> GetWorkoutNames(
        [FromRoute] int userId,
        [FromQuery] GetWorkoutGraphDataRequest request
    )
    {
        Result<GetWorkoutGraphDataResponse> getWorkoutGraphDataResponse =
            await _workoutGraphDataService.GetWorkoutGraphData(request, userId);
        return getWorkoutGraphDataResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getWorkoutGraphDataResponse.Error))
            : Ok(getWorkoutGraphDataResponse.Value);
    }
}