using FitnessTracker.Contracts.Requests.Users;
using FitnessTracker.Contracts.Requests.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Contracts.Requests.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.Users;
using FitnessTracker.Contracts.Responses.WorkoutGraphData.GetWorkoutGraphData;
using FitnessTracker.Contracts.Responses.WorkoutNames.GetWorkoutNames;
using FitnessTracker.Interfaces.Services.User;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Users;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IWorkoutNamesService _workoutNamesService;
    private readonly IWorkoutGraphDataService _workoutGraphDataService;

    public UserController(IUserService userService, IWorkoutNamesService workoutNamesService, IWorkoutGraphDataService workoutGraphDataService)
    {
        _userService = userService;
        _workoutNamesService = workoutNamesService;
        _workoutGraphDataService = workoutGraphDataService;
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request,
        [FromServices] IValidator<LoginRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<LoginResponse> loginResponse = await _userService.LoginAsync(request);
        return !loginResponse.IsSuccess
            ? BadRequest(new ErrorResponse(loginResponse.Error))
            : Ok(loginResponse.Value);
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(RegisterResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request, 
        [FromServices] IValidator<RegisterRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        Result<RegisterResponse> registerResponse = await _userService.RegisterAsync(request);
        return !registerResponse.IsSuccess
            ? BadRequest(new ErrorResponse(registerResponse.Error))
            : Ok(registerResponse.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetUser(int id)
    {
        Result<GetUserResponse> user = await _userService.GetUserAsync(id);
        return !user.IsSuccess
            ? BadRequest(new ErrorResponse(user.Error))
            : Ok(user.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateUserResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        Result<UpdateUserResponse> updateUserResponse = await _userService.UpdateUserAsync(id, request);
        return !updateUserResponse.IsSuccess
            ? BadRequest(new ErrorResponse(updateUserResponse.Error))
            : Ok(updateUserResponse.Value);
    }

    [HttpGet("{id}/WorkoutNames")]
    [ProducesResponseType(typeof(GetWorkoutNamesResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetWorkoutNames(
        [FromRoute] int id,
        [FromQuery] GetWorkoutNamesRequest request
    )
    {
        Result<GetWorkoutNamesResponse> getWorkoutNamesResponse = await _workoutNamesService.GetWorkoutNames(id, request);
        return getWorkoutNamesResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getWorkoutNamesResponse.Error))
            : Ok(getWorkoutNamesResponse.Value);
    }

    [HttpGet("{id}/WorkoutGraphData")]
    [ProducesResponseType(typeof(GetWorkoutGraphDataResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetWorkoutNames(
        [FromRoute] int id,
        [FromQuery] GetWorkoutGraphDataRequest request
    )
    {
        Result<GetWorkoutGraphDataResponse> getWorkoutGraphDataResponse = await _workoutGraphDataService.GetWorkoutGraphData(request, id);
        return getWorkoutGraphDataResponse.IsSuccess is false
            ? BadRequest(new ErrorResponse(getWorkoutGraphDataResponse.Error))
            : Ok(getWorkoutGraphDataResponse.Value);
    }

    [HttpGet("{id}/Achievements")]
    [ProducesResponseType(typeof(List<IUserAchievement>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetUserAchievementsAsync(int id)
    {
        Result<GetUserResponse> user = await _userService.GetUserAsync(id);
        return user.Match<IActionResult>(
            success => Ok(success.User.WorkoutBuddy.Data.UserAchievements),
            failure => BadRequest(new ErrorResponse(failure))
        );
    }

    [HttpPost("{id}/RecordAchievement/{achievementId}")]
    [ProducesResponseType(typeof(RecordAchievementResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> RecordAchievement(int id, int achievementId)
    {
        Result<RecordAchievementResponse> recordAchievementResponse = await _userService.RecordAchievementAsync(id, achievementId);
        return recordAchievementResponse.Match<IActionResult>(
            success => Ok(success),
            failure => BadRequest(new ErrorResponse(failure))
        );
    }
}