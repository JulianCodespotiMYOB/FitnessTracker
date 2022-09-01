using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Exercise;

[ApiController]
[Route("Exercises")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public IActionResult GetExercises()
    {
        Result<GetExercisesResponse> exercisesResponse = _exerciseService.GetExercises();
        return !exercisesResponse.IsSuccess
            ? BadRequest(new ErrorResponse(exercisesResponse.Error))
            : Ok(exercisesResponse.Value);
    }

    [HttpPost]
    public IActionResult PostExercises()
    {
        Result<PostExercisesResponse> exercisesResponse = _exerciseService.PostExercises();
        return !exercisesResponse.IsSuccess
            ? BadRequest(new ErrorResponse(exercisesResponse.Error))
            : Ok(exercisesResponse.Value);
    }
}