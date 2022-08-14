﻿using FitnessTracker.Interfaces.Infrastructure;
using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Application.Common;

public class UserHelper
{
    public static async Task<Result<User>> GetUserFromDatabaseById(int userId, IApplicationDbContext context,
        ILogger logger)
    {
        User? user = await context.Users
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Data)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .Include(u => u.WorkoutBuddy)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            logger.LogError($"User with id {userId} not found");
            return Result<User>.Failure("User not found");
        }

        return Result<User>.Success(user);
    }

    public static async Task<Result<User>> GetUserFromDatabaseByEmail(string userEmail, IApplicationDbContext context,
        ILogger logger)
    {
        User? user = await context.Users
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Data)
            .Include(u => u.Workouts)
            .ThenInclude(w => w.Activities)
            .ThenInclude(a => a.Exercise)
            .Include(u => u.WorkoutBuddy)
            .FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user is null)
        {
            logger.LogError($"User with id {userEmail} not found");
            return Result<User>.Failure("User not found");
        }

        return Result<User>.Success(user);
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