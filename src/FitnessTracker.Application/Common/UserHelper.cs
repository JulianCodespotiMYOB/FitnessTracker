using FitnessTracker.Interfaces;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Common;

public class UserHelper
{
    public static async Task<Result<User>> GetUserFromDatabase(int userId, IApplicationDbContext context, ILogger logger)
    {
        await context.Users.ForEachAsync(u => Console.WriteLine(u.Id));
        User? user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            logger.LogError($"User with id {userId} not found");
            return Result<User>.Failure("User not found");
        }

        return Result<User>.Success(user);
    }
}