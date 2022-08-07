using FitnessTracker.Contracts.Responses;
using FitnessTracker.Contracts.Responses.Exercises;
using FitnessTracker.Interfaces;
using FitnessTracker.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers;

[ApiController]
[Route("Exercises")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        this.exerciseService = exerciseService;
    }

    [HttpGet]
    public IActionResult GetExercises()
    {
        Result<GetExercisesResponse> exercisesResponse = exerciseService.GetExercises();
        return !exercisesResponse.IsSuccess
            ? BadRequest(new ErrorResponse(exercisesResponse.Error))
            : Ok(exercisesResponse.Value);
    }
}