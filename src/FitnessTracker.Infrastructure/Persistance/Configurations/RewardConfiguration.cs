using FitnessTracker.Models.Users;
using FitnessTracker.Models.Users.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessTracker.Infrastructure.Persistance.Configurations;

public class RewardConfiguration : IEntityTypeConfiguration<Reward>
{
    public void Configure(EntityTypeBuilder<Reward> builder)
    {
        builder.HasDiscriminator(r => r.RewardType)
            .HasValue<Experience>(RewardTypes.Experience)
            .HasValue<Badge>(RewardTypes.Badge)
            .HasValue<Title>(RewardTypes.Title);
    }
}