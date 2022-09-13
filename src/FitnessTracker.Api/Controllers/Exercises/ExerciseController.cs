using FitnessTracker.Contracts.Responses.Common;
using FitnessTracker.Contracts.Responses.Exercises.GetExercises;
using FitnessTracker.Contracts.Responses.Exercises.PostExercises;
using FitnessTracker.Interfaces.Services.Exercises;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Exercises;

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