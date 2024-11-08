using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Reference)
            .IsRequired();

        builder.HasOne(x => x.Channel)
            .WithMany(x => x.Links)
            .HasForeignKey(x => x.ChannelId);
    }
}