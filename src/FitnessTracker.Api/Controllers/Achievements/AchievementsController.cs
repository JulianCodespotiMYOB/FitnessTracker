using FitnessTracker.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Api.Controllers.Exercises;

[ApiController]
[Route("Achievements")]
public class AchievementsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<Achievement>), 200)]
    public IActionResult GetAllAchievements()
    {
        return Ok(Achievements.AllAchievements);
    }
}