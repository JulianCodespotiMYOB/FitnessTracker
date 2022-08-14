using FitnessTracker.Models.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Interfaces.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}