using FitnessTracker.Models.Authorization;
using FitnessTracker.Models.Buddy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessTracker.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.WorkoutBuddy)
            .WithOne(wb => wb.User)
            .HasForeignKey<WorkoutBuddy>(wb => wb.Id);
    }
}