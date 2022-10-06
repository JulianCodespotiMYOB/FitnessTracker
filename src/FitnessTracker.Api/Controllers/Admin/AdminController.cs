using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.Exercises.PostExercises;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Interfaces.Services.Exercises;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Exercises;

[ApiController]
[Route("Admin")]
public class AdminController : ControllerBase
{
    private readonly IExerciseService _exerciseService;
    private readonly IUserService _userService;

    public AdminController(IExerciseService exerciseService, IUserService userService)
    {
        _exerciseService = exerciseService;
        _userService = userService;
    }

    [HttpPost]
    [Route("SeedExercises")]
    [ProducesResponseType(typeof(PostExercisesResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> SeedExercises()
    {
        Result<PostExercisesResponse> exercisesResponse = await _exerciseService.PostExercisesAsync();
        return !exercisesResponse.IsSuccess
            ? BadRequest(new ErrorResponse(exercisesResponse.Error))
            : Ok(exercisesResponse.Value);
    }

    [HttpGet]
    [Route("GetAllUsers")]
    [ProducesResponseType(typeof(GetUsersResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAllUsers()
    {
        Result<GetUsersResponse> users = await _userService.GetUsersAsync();
        return !users.IsSuccess
            ? BadRequest(new ErrorResponse(users.Error))
            : Ok(users.Value);
    }
}