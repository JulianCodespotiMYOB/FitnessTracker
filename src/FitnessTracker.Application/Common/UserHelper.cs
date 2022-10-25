using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Models.Common;
using FitnessTracker.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Common;

public static class UserHelper
{
    public static async Task<User?> GetUserFromDatabaseById(int userId, IApplicationDbContext context)
    {
        return await context.Users
            .Include(u => u.Avatar)
            .Include(u => u.UserSettings)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Data)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .Include(u => u.WorkoutBuddy)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public static async Task<User?> GetUserFromDatabaseByEmail(string userEmail, IApplicationDbContext context)
    {
        return await context.Users
            .Include(u => u.Avatar)
            .Include(u => u.UserSettings)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Data)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .Include(u => u.WorkoutBuddy)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == userEmail.ToLower());
    }

    public static async Task<Result<IEnumerable<User>>> GetUsersFromDatabase(IApplicationDbContext applicationDbContext,
        ILogger logger)
    {
        List<User>? users = await applicationDbContext.Users
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Data)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .Include(u => u.WorkoutBuddy)
            .ToListAsync();

        if (users is null)
        {
            logger.LogError("Users not found");
            return Result<IEnumerable<User>>.Failure("Users not found");
        }

        return Result<IEnumerable<User>>.Success(users);
    }
}