using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Common;

public static class UserHelper
{
    public static async Task<User?> GetUserFromDatabaseById(int userId, IApplicationDbContext context)
    {
        return await context.Users
            .Include(u => u.Maxes)
            .Include(u => u.Inventory)
            .Include(u => u.Avatar)
            .Include(u => u.UserSettings)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Data)
            .ThenInclude(d => d.Image)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .ThenInclude(e => e.MuscleGroupImage)
            .Include(u => u.WorkoutBuddy)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public static async Task<User?> GetUserFromDatabaseByEmail(string userEmail, IApplicationDbContext context)
    {
        return await context.Users
            .Include(u => u.Maxes)
            .Include(u => u.Inventory)
            .Include(u => u.Avatar)
            .Include(u => u.UserSettings)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Data)
            .ThenInclude(d => d.Image)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .ThenInclude(e => e.MuscleGroupImage)
            .Include(u => u.WorkoutBuddy)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == userEmail.ToLower());
    }

    public static async Task<IEnumerable<User>> GetUsersFromDatabase(IApplicationDbContext applicationDbContext)
    {
        try
        {
            return await applicationDbContext.Users
                .Include(u => u.Maxes)
                .Include(u => u.Inventory)
                .Include(u => u.Avatar)
                .Include(u => u.UserSettings)
                .Include(u => u.Workouts)
                .ThenInclude(w => w.Activities)
                .ThenInclude(a => a.Data)
                .ThenInclude(d => d.Image)
                .Include(u => u.Workouts)
                .ThenInclude(w => w.Activities)
                .ThenInclude(a => a.Exercise)
                .ThenInclude(e => e.MuscleGroupImage)
                .Include(u => u.WorkoutBuddy)
                .ToListAsync();
        }
        catch (Exception e)
        {
            return new List<User>();
        }
    }
}