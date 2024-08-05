using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class PremiumConfiguration : IEntityTypeConfiguration<Premium>
{
    public void Configure(EntityTypeBuilder<Premium> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.HasOne(s => s.User)
            .WithOne(u => u.Subscriptions)
            .HasForeignKey<Premium>(s => s.UserId);
    }
}