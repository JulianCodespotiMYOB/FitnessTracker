using FitnessTracker.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Exercises;

[ApiController]
[Route("Achievements")]
public class AchievementsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<IAchievement>), 200)]
    public IActionResult GetAllAchievements()
    {
        return Ok(Achievements.AllAchievements);
    }
}