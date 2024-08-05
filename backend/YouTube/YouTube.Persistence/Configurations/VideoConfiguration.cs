using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.ViewCount)
            .HasDefaultValue(0);

        builder.Property(x => x.LikeCount)
            .HasDefaultValue(0);

        builder.Property(x => x.DisLikeCount)
            .HasDefaultValue(0);

        builder.Property(x => x.ReleaseDate);

        builder.Property(x => x.IsHidden)
            .IsRequired();

        builder.Property(x => x.Age)
            .HasDefaultValue(0);

        builder.HasOne(x => x.Channel)
            .WithMany(x => x.Videos)
            .HasForeignKey(x => x.ChannelId);

        builder.HasOne(x => x.VideoUrl)
            .WithOne()
            .HasForeignKey<Video>(x => x.VideoUrlId);

        builder.HasOne(x => x.PreviewImg)
            .WithOne()
            .HasForeignKey<Video>(x => x.PreviewImgId);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Video)
            .HasForeignKey(x => x.VideoId);

        builder.HasOne(x => x.Category)
            .WithOne()
            .HasForeignKey<Video>(x => x.CategoryId);

        builder.HasMany(x => x.Playlists)
            .WithMany(x => x.Videos);
    }
}