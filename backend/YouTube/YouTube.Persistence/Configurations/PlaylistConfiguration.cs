using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsHidden);

        builder.HasOne(x => x.Channel)
            .WithMany(x => x.Playlists)
            .HasForeignKey(x => x.ChannelId);
    }
}