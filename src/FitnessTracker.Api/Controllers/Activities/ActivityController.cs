using FitnessTracker.Contracts.Requests.WorkoutVolume;
using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.WorkoutVolume;
using FitnessTracker.Interfaces.Services.Activities;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Activities;

[ApiController]
[Route("Users")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivityController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet("{userId:int}/ActivityVolume/{activityId:int}")]
    [ProducesResponseType(typeof(GetActivityVolumeResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetActivityVolume(
        [FromRoute] GetActivityVolumeRequest request
    )
    {
        Result<GetActivityVolumeResponse> getActivityVolumeResponse = await _activityService.GetActivityVolume(request);
        return getActivityVolumeResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getActivityVolumeResponse.Error))
            : Ok(getActivityVolumeResponse.Value);
    }
}